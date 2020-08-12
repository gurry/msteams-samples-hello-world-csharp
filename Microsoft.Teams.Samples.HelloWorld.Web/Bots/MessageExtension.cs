using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Microsoft.Bot.Builder.Teams;
using Microsoft.Bot.Schema.Teams;
using Newtonsoft.Json.Linq;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using AdaptiveCards;
using Bogus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Tachyon.SDK.Consumer.Api;
using Tachyon.SDK.Consumer.DefaultImplementations;
using Tachyon.SDK.Consumer.Models.Api;
using Tachyon.SDK.Consumer.Models.Receive;

namespace Microsoft.Teams.Samples.HelloWorld.Web
{
    public class MessageExtension : TeamsActivityHandler
    {
        private readonly ILogger<MessageExtension> _logger;
        private readonly HttpClient _httpClient;
        private readonly string _tachyonHostName;
        private DefaultTransportProxy _transportProxy;
        private LogProxy<MessageExtension> _logProxy;
        private const string ConsumerHeader = "X-Tachyon-Consumer";
        private const string ConsumerName = "Teams";
        private static string _lastDevice;

        public MessageExtension(IConfiguration config, ILogger<MessageExtension> logger)
        {
            _logger = logger;
            _tachyonHostName = config["TachyonHostname"];
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add(ConsumerHeader, ConsumerName);
            _logProxy = new LogProxy<MessageExtension>(logger);
            _transportProxy = new DefaultTransportProxy(_logProxy, ConsumerName, $"http://{_tachyonHostName}/Consumer/");
        }

        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            turnContext.Activity.RemoveRecipientMention();

            if (string.IsNullOrWhiteSpace(turnContext.Activity.Text))
            {
                return;
            }

            var text = turnContext.Activity.Text.Trim().ToLower();

            if (text.StartsWith("device "))
            {
                var parts = text.Split();
                if (parts.Length != 2)
                {
                    const string replyText = "Wrong syntax. Try \"device &lt;device FQDN&gt;\"";
                    _logger.LogError($"Bad syntax: '{text}'");
                    await turnContext.SendActivityAsync(MessageFactory.Text(replyText), cancellationToken);
                    return;
                }

                var fqdn = parts[1];

                var device = await GetDeviceAsync(fqdn);

                if (device == null)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Sorry, could not find any information about device \"{fqdn}\" ☹"), cancellationToken);
                    return;
                }

                var card = CreateDeviceCard(device);

                await turnContext.SendActivityAsync(MessageFactory.Text($"Here's the device information from Tachyon:"), cancellationToken);
                await turnContext.SendActivityAsync(MessageFactory.Attachment(card), cancellationToken);

                _lastDevice = fqdn;
            }
            else if (text.StartsWith("instr "))
            {
                var parts = text.Split();
                if (parts.Length != 2)
                {
                    _logger.LogError($"Bad syntax: '{text}'");
                    const string replyText = "Wrong syntax. Try \"instr &lt;instruction ID&gt;\"";
                    await turnContext.SendActivityAsync(MessageFactory.Text(replyText), cancellationToken);
                    return;
                }

                var instructionId = parts[1];

                string response;
                try
                {
                    response = RunInstruction(instructionId);
                }
                catch (Exception e)
                {
                    response = $"Exception while running instruction: {e}";
                }

                if (string.IsNullOrWhiteSpace(response))
                {
                    response = "There was an error while running instruction ☹";
                }

                await turnContext.SendActivityAsync(MessageFactory.Text("Instruction result:"), cancellationToken);
                await turnContext.SendActivityAsync(MessageFactory.Text(response), cancellationToken);
            }
        }

        private async Task<Device> GetDeviceAsync(string fqdn)
        {
            var data = System.Text.Encoding.UTF8.GetBytes(fqdn);
            var base64Fqdn = System.Convert.ToBase64String(data);

            var urlPath = $"consumer/devices/fqdn/{base64Fqdn}";
            var response = await CallTachyon(HttpMethod.Get, urlPath);

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                _logger.LogWarning($"Device {fqdn} not found in Tachyon");
                return null;
            }

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"Received {response.StatusCode} from Tachyon");
                throw new Exception($"Received {response.StatusCode} from Tachyon");
            }

            var deviceJson = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions();
            options.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
            try
            {
                return System.Text.Json.JsonSerializer.Deserialize<Device>(deviceJson, options);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private string RunInstruction(string instructionName)
        {

            var instructionDefs = new InstructionDefinitions(_transportProxy, _logProxy);
            var instructionDef = MakeTachyonCall(() => instructionDefs.GetInstructionDefinition(instructionName));

            var instructions = new Instructions(_transportProxy, _logProxy);
            var instruction = MakeTachyonCall(() =>
                instructions.SendTargetedInstruction(instructionDef.Id, null, 10, 10, new List<string> {"Client2.ntl.local"}));

            var instructionStatus = (TachyonInstructionStatus) instruction.Status;
            if (instructionStatus != TachyonInstructionStatus.Approved &&
                instructionStatus != TachyonInstructionStatus.Authenticating &&
                instructionStatus != TachyonInstructionStatus.Complete &&
                instructionStatus != TachyonInstructionStatus.Created &&
                instructionStatus != TachyonInstructionStatus.InApproval &&
                instructionStatus != TachyonInstructionStatus.InProgress &&
                instructionStatus != TachyonInstructionStatus.Sent)
            {
                throw new Exception($"Bad instruction status {instructionStatus}");
            }

            var responses = new Responses(_transportProxy, _logProxy);


            var maxWait = TimeSpan.FromSeconds(80);

            var startTime = DateTime.Now;
            var elapsed = TimeSpan.FromSeconds(0);
            Dictionary<string, object> response = null;
            while (elapsed < maxWait)
            {
                response = MakeTachyonCall(() => responses.GetProcessedResponses(instruction.Id));

                if (response != null && response.Count != 0)
                {
                    break;
                }

                elapsed = DateTime.Now - startTime;
            }

            if (response == null)
            {
                return null;
            }
        
            return System.Text.Json.JsonSerializer.Serialize(response);
        }

        private Attachment CreateDeviceCard(Device device)
        {
            //string cardJson =
            //    "{\r\n  \"$schema\": \"http://adaptivecards.io/schemas/adaptive-card.json\",\r\n  \"type\": \"AdaptiveCard\",\r\n  \"version\": \"1.0\",\r\n  \"body\": [\r\n        {\r\n      \"type\": \"Container\",\r\n      \"items\": [\r\n        {\r\n          \"type\": \"TextBlock\",\r\n          \"text\": \"Client2.ntl.local\",\r\n          \"weight\": \"bolder\",\r\n          \"size\": \"large\"\r\n        },\r\n        {\r\n          \"type\": \"ColumnSet\",\r\n          \"columns\": [\r\n              {\r\n                \"type\": \"Column\",\r\n                \"width\": \"auto\",\r\n                \"items\": [\r\n                  {\r\n                    \"type\": \"TextBlock\",\r\n                    \"text\": \"•\",\r\n                    \"size\": \"extralarge\",\r\n                    \"color\": \"good\"\r\n                  }\r\n                ],\r\n                \"verticalContentAlignment\": \"Center\"\r\n              },\r\n              {\r\n                \"type\": \"Column\",\r\n                \"width\": \"auto\",\r\n                \"items\": [\r\n                  {\r\n                    \"type\": \"TextBlock\",\r\n                    \"text\": \"Currently Online\",\r\n                    \"size\": \"small\",\r\n                    \"isSubtle\": true\r\n                  }\r\n                ],\r\n                \"verticalContentAlignment\": \"Center\"\r\n              }\r\n            ]\r\n          \r\n         \r\n        },\r\n        {\r\n          \"type\": \"FactSet\",\r\n          \"facts\": [\r\n            {\r\n              \"title\": \"Name:\",\r\n              \"value\": \"Client2\"\r\n            },\r\n            {\r\n              \"title\": \"FQDN:\",\r\n              \"value\": \"Client2.ntl.local\"\r\n            },\r\n            {\r\n              \"title\": \"IP Address:\",\r\n              \"value\": \"10.1.10.20\"\r\n            },\r\n            {\r\n              \"title\": \"OS:\",\r\n              \"value\": \"Windows 10\"\r\n            },\r\n            {\r\n              \"title\": \"Device Type:\",\r\n              \"value\": \"Desktop\"\r\n            },\r\n            {\r\n              \"title\": \"Manufacturer:\",\r\n              \"value\": \"Dell Inc.\"\r\n            },\r\n            {\r\n              \"title\": \"Model:\",\r\n              \"value\": \"Precision T5600\"\r\n            },\r\n            {\r\n              \"title\": \"Serial Number:\",\r\n              \"value\": \"3C17L72\"\r\n            },\r\n            {\r\n              \"title\": \"Domain:\",\r\n              \"value\": \"NTL.local\"\r\n            },\r\n            {\r\n              \"title\": \"Location:\",\r\n              \"value\": \"Noida\"\r\n            },\r\n            {\r\n              \"title\": \"Last Boot Time (UTC):\",\r\n              \"value\": \"2019-02-04T00:00:00Z\"\r\n            }\r\n          ]\r\n        }\r\n      ]\r\n    }  \r\n  ]\r\n}\r\n";

            var card = new AdaptiveCard(new AdaptiveSchemaVersion(1, 0))
            {
                Body = new List<AdaptiveElement>
                {
                    new AdaptiveContainer
                    {
                        Items = new List<AdaptiveElement>
                        {
                            new AdaptiveTextBlock(device.Fqdn)
                            {
                                Weight = AdaptiveTextWeight.Bolder, 
                                Size = AdaptiveTextSize.Large
                            },
                            new AdaptiveColumnSet
                            {
                                Columns = new List<AdaptiveColumn>
                                {
                                    new AdaptiveColumn
                                    {
                                        Width = "auto",
                                        VerticalContentAlignment = AdaptiveVerticalContentAlignment.Center,
                                        Items = new List<AdaptiveElement> 
                                        {
                                            new AdaptiveTextBlock("•")
                                            {
                                                Size = AdaptiveTextSize.ExtraLarge,
                                                Color = device.IsActive() ? AdaptiveTextColor.Good : AdaptiveTextColor.Attention
                                            },
                                        }
                                    },
                                    new AdaptiveColumn
                                    {
                                        Width = "auto",
                                        VerticalContentAlignment = AdaptiveVerticalContentAlignment.Center,
                                        Items = new List<AdaptiveElement> 
                                        {
                                            new AdaptiveTextBlock(device.IsActive() ? "Online" : "Offline")
                                            {
                                                Size = AdaptiveTextSize.Medium,
                                                IsSubtle = true
                                            },
                                        }
                                    }
                                }
                            },
                            new AdaptiveFactSet
                            {
                                Facts = new List<AdaptiveFact>
                                {
                                    new AdaptiveFact("FQDN:", device.Fqdn ?? "Unknown"),
                                    //new AdaptiveFact("Name:", device.Name ?? "Unknown"),
                                    new AdaptiveFact("IP", device.LocalIpAddress?.Split("/")[0] ?? "Unknown"),
                                    new AdaptiveFact("Subnet:", device.LocalIpAddress ?? "Unknown"),
                                    new AdaptiveFact("Domain:", device.Domain ?? "Unknown"),
                                    new AdaptiveFact("OS:", device.OsVerTxt ?? "Unknown"),
                                    new AdaptiveFact("Device Type:", AgentDeviceType.AsString(device.DeviceType)),
                                    new AdaptiveFact("CPU Architecture:", device.CpuArchitecture ?? "Unknown"),
                                    new AdaptiveFact("Manufacturer:", device.Manufacturer ?? "Unknown"),
                                    new AdaptiveFact("Model:", device.Model ?? "Unknown"),
                                    new AdaptiveFact("Serial No.:", device.SerialNumber ?? "Unknown"),
                                    new AdaptiveFact("BIOS Guid:", device.SmBiosGuid.ToString()),
                                    new AdaptiveFact("Current Timezone:", device.TimeZoneId),
                                    new AdaptiveFact("Last boot time (UTC):", device.LastBootUTC.ToString(CultureInfo.CurrentCulture)),
                                }
                            }
                        }
                    },
                }
            };


            return new Attachment
            {
                ContentType = AdaptiveCard.ContentType,
                Content = card
            };
        }

        private async Task<HttpResponseMessage> CallTachyon(HttpMethod method, string urlPath, string body = null)
        {
            var request = new HttpRequestMessage(method, $"http://{_tachyonHostName}/{urlPath}");
            if (body != null)
            {
                request.Content = new StringContent(body);
            }

            HttpResponseMessage response;
            try
            {
                response = await _httpClient.SendAsync(request);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception while calling Tachyon");
                throw;
            }

            return response;
        }


        private string ToTimeZoneString(int offsetMins)
        {
            var sign = offsetMins >= 0 ? "+" : "-";

            var offsetMinsAbs = Math.Abs(offsetMins);
            var hours = offsetMinsAbs / 60;
            var minutes = offsetMinsAbs % 60;

            return $"{sign}{hours}:{minutes}";
        }


        protected override Task<MessagingExtensionResponse> OnTeamsMessagingExtensionQueryAsync(ITurnContext<IInvokeActivity> turnContext, MessagingExtensionQuery query, CancellationToken cancellationToken)
        {
            var title = "";
            var titleParam = query.Parameters?.FirstOrDefault(p => p.Name == "cardTitle");
            if (titleParam != null)
            {
                title = titleParam.Value.ToString();
            }

            if (query == null)
            {
                // We only process the 'getRandomText' queries with this message extension
                throw new NotImplementedException($"Invalid CommandId: {query.CommandId}");
            }

            var attachments = new MessagingExtensionAttachment[5];

            attachments[0] = GetAttachment("Adobe Acrobat Reader");
            attachments[1] = GetAttachment("Upgrade to Windows 10");
            attachments[2] = GetAttachment("Camtasia 7.1");
            attachments[3] = GetAttachment("Office 365");
            attachments[4] = GetAttachment("Floppy Bird");
            

            var result = new MessagingExtensionResponse
            {
                ComposeExtension = new MessagingExtensionResult
                {
                    AttachmentLayout = "list",
                    Type = "result",
                    Attachments = attachments.ToList()
                },
            };
            return Task.FromResult(result);

        }

        private static MessagingExtensionAttachment GetAttachment(string title)
        {
            var card = new ThumbnailCard
            {
                Title = !string.IsNullOrWhiteSpace(title) ? title : new Faker().Lorem.Sentence(),
                Text = new Faker().Lorem.Paragraph(),
                Images = new List<CardImage> { new CardImage("http://lorempixel.com/640/480?rand=" + DateTime.Now.Ticks) }
            };

            return card
                .ToAttachment()
                .ToMessagingExtensionAttachment();
        }

        protected override Task<MessagingExtensionResponse> OnTeamsMessagingExtensionSelectItemAsync(ITurnContext<IInvokeActivity> turnContext, JObject query, CancellationToken cancellationToken)
        {

            return Task.FromResult(new MessagingExtensionResponse
            {
                ComposeExtension = new MessagingExtensionResult
                {
                    AttachmentLayout = "list",
                    Type = "result",
                    Attachments = new MessagingExtensionAttachment[]{
                        new ThumbnailCard()
                            .ToAttachment()
                            .ToMessagingExtensionAttachment()
                    }
                },
            });
        }

        private  static T MakeTachyonCall<T>(Func<ApiCallResponse<T>> call) where T : class
        {
            ApiCallResponse<T> response;
            try
            {
                response = call();
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception while calling Tachyon: {ex}", ex);
            }

            if (response == null)
            {
                throw new Exception("Tachyon returned null response");
            }

            if (!response.Success)
            {
                throw new Exception($"Tachyon returned an error response {response.Errors}");
            }

            var receivedObject = response.ReceivedObject;

            if (receivedObject == null)
            {
                throw new Exception("Tachyon returned response with null 'ReceivedObj'");
            }

            return receivedObject;
        }
    }
}
