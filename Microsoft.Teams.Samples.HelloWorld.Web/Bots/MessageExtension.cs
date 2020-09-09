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
using Microsoft.Bot.Connector;
using Microsoft.Bot.Connector.Authentication;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Teams.Samples.HelloWorld.Web.Bots;
using Tachyon.SDK.Consumer.Api;
using Tachyon.SDK.Consumer.DefaultImplementations;
using Tachyon.SDK.Consumer.Enums;
using Tachyon.SDK.Consumer.Models.Api;
using Tachyon.SDK.Consumer.Models.Common;
using Tachyon.SDK.Consumer.Models.Receive;
using Task = System.Threading.Tasks.Task;

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
        private static string _selectedDevice;
        private readonly InstructionDefinitions _instructionDefs;
        private static string _lastDeviceQueried;
        public static ConnectorClient _connectorClient = new ConnectorClient(new Uri("https://smba.trafficmanager.net/in/"), new MicrosoftAppCredentials("2ea52b84-e497-4bcb-8b5d-9670b57c2915", "-a6Sd~nx~a8vXs3Z820L_t9m3JluvFVl1-"));
        public static Activity _reply;

        private static IList<InstructionHint> _lastReturnedInstructions;

        public MessageExtension(IConfiguration config, ILogger<MessageExtension> logger)
        {
            _logger = logger;
            _tachyonHostName = config["TachyonHostname"];
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add(ConsumerHeader, ConsumerName);
            _logProxy = new LogProxy<MessageExtension>(logger);
            _transportProxy = new DefaultTransportProxy(_logProxy, ConsumerName, $"http://{_tachyonHostName}/Consumer/");
            _instructionDefs = new InstructionDefinitions(_transportProxy, _logProxy);
        }

        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            var activity = turnContext.Activity as Activity;
            _reply = activity.CreateReply();

            turnContext.Activity.RemoveRecipientMention();

            var originalText = turnContext.Activity.Text?.Trim();
            if (originalText == null)
            {
                return;
            }

            var text = originalText.ToLower() ?? "";

            if (text.StartsWith("testing"))
            {
                return;
            }
            else if (text.StartsWith("info "))
            {
                await turnContext.SendActivityAsync(CreateTypingActivity(turnContext), cancellationToken);

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
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Sorry, could not find any information about '{fqdn}' ☹"), cancellationToken);
                    return;
                }

                var card = CreateDeviceCard(device);

                _lastDeviceQueried = fqdn;

                await turnContext.SendActivityAsync(MessageFactory.Text("Here are the device details:"), cancellationToken);
                await turnContext.SendActivityAsync(MessageFactory.Attachment(card), cancellationToken);

                await Task.Delay(2000);
                var actionCard = ConnectorController.CreateSelectedTicketActionCard();
                if (actionCard != null)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Attachment(actionCard), cancellationToken);
                }
            }
            else if (text.StartsWith("select"))
            {
                await turnContext.SendActivityAsync(CreateTypingActivity(turnContext), cancellationToken);

                var parts = text.Split();
                string selectedDevice = null;
                if (parts.Length == 1)
                {
                    selectedDevice = _lastDeviceQueried;
                }
                else if (parts.Length == 2 && !string.IsNullOrWhiteSpace(parts[1]))
                {
                    selectedDevice = parts[1];
                }

                if (selectedDevice == null)
                {
                    _logger.LogError($"Bad syntax: '{text}'");
                    const string replyText = "Wrong syntax. Try \"select &lt;device FQDN&gt;\"";
                    await turnContext.SendActivityAsync(MessageFactory.Text(replyText), cancellationToken);
                    return;
                }


                _selectedDevice = selectedDevice;

                await turnContext.SendActivityAsync(CreateTypingActivity(turnContext), cancellationToken);
                var device = await GetDeviceAsync(_selectedDevice);

                if (device == null)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Sorry, could not find any information about '{_selectedDevice}' ☹"), cancellationToken);
                    return;
                }

                if (device.IsActive())
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text("The device is currently online. You can run instructions against it by typing them here 👍 "), cancellationToken);
                }
                else
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text("Can't run instructions. The device is currently offline"), cancellationToken);
                }
            }
            else if (text== "unselect")
            {
                if (_selectedDevice != null)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Device '{_selectedDevice}' unselected"), cancellationToken);
                    _selectedDevice = null;
                }
            }
            else if (text.StartsWith("availability"))
            {
                await turnContext.SendActivityAsync(CreateTypingActivity(turnContext), cancellationToken);
                await Task.Delay(600);

                var card = CreateUserScheduleCard();

                await turnContext.SendActivityAsync(MessageFactory.Text("User is available at these times today:"), cancellationToken);

                await turnContext.SendActivityAsync(MessageFactory.Attachment(card), cancellationToken);

            }
            else if (text.StartsWith("unlockaccount"))
            {
                var username = originalText.Remove(0, "unlockaccount".Length).Trim();

                if (string.IsNullOrWhiteSpace(username))
                {
                    _logger.LogError("Bad syntax. Missing user name");
                    await turnContext.SendActivityAsync(MessageFactory.Text("Bad syntax. Missing user name"), cancellationToken);
                    return;
                }
                


                await turnContext.SendActivityAsync(CreateTypingActivity(turnContext), cancellationToken);

                await Task.Delay(200);
                await turnContext.SendActivityAsync(MessageFactory.Text($"Unlocking account of user {username}..."), cancellationToken);

                await turnContext.SendActivityAsync(CreateTypingActivity(turnContext), cancellationToken);
                await Task.Delay(1500);
                await turnContext.SendActivityAsync(MessageFactory.Text("Unlocked account and notified the user ✔"), cancellationToken);

                await turnContext.SendActivityAsync(CreateTypingActivity(turnContext), cancellationToken);

                await Task.Delay(200);
                var closeTicketCard = CreateTicketCloseCard();
                await turnContext.SendActivityAsync(MessageFactory.Attachment(closeTicketCard), cancellationToken);

            }
            else if (text.StartsWith("connectivity"))
            {
                var deviceName = originalText.Remove(0, "connectivity".Length).Trim();

                if (string.IsNullOrWhiteSpace(deviceName))
                {
                    _logger.LogError("Bad syntax. Missing device name");
                    await turnContext.SendActivityAsync(MessageFactory.Text("Bad syntax. Missing device name"), cancellationToken);
                    return;
                }
                

                await turnContext.SendActivityAsync(CreateTypingActivity(turnContext), cancellationToken);

                await Task.Delay(200);
                await turnContext.SendActivityAsync(MessageFactory.Text($"Checking connectivity on device {deviceName}..."), cancellationToken);

                for (int i = 0; i < 4; i++)
                {
                    await Task.Delay(1500);
                    await turnContext.SendActivityAsync(CreateTypingActivity(turnContext), cancellationToken);
                }

                await Task.Delay(200);

                var card = CreateConnectivityReportCard(deviceName);
                await turnContext.SendActivityAsync(MessageFactory.Attachment(card), cancellationToken);
                await turnContext.SendActivityAsync(CreateTypingActivity(turnContext), cancellationToken);

                await Task.Delay(2000);
                var actionCard = ConnectorController.CreateSelectedTicketActionCard();
                if (actionCard != null)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Attachment(actionCard), cancellationToken);
                }
            }
            else if (text.StartsWith("vpnstatus"))
            {
                await turnContext.SendActivityAsync(CreateTypingActivity(turnContext), cancellationToken);

                await Task.Delay(200);
                await turnContext.SendActivityAsync(MessageFactory.Text($"Getting VPN server health report..."), cancellationToken);

                for (int i = 0; i < 4; i++)
                {
                    await Task.Delay(1500);
                    await turnContext.SendActivityAsync(CreateTypingActivity(turnContext), cancellationToken);
                }

                await Task.Delay(200);

                var card = CreateVpnHealthCard();
                await turnContext.SendActivityAsync(MessageFactory.Attachment(card), cancellationToken);
                await turnContext.SendActivityAsync(CreateTypingActivity(turnContext), cancellationToken);

                await Task.Delay(2000);
                var actionCard = ConnectorController.CreateSelectedTicketActionCard();
                if (actionCard != null)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Attachment(actionCard), cancellationToken);
                }
            }
            else if (text.StartsWith("onboard"))
            {
                var newJoiner = originalText.Remove(0, "onboard".Length).Trim();

                if (string.IsNullOrWhiteSpace(newJoiner))
                {
                    _logger.LogError("Bad syntax. Missing leaver name");
                    await turnContext.SendActivityAsync(MessageFactory.Text("Bad syntax. Missing leaver name"), cancellationToken);
                    return;
                }

                await turnContext.SendActivityAsync(MessageFactory.Text($"Starting onboarding process for {newJoiner}..."), cancellationToken);
                await turnContext.SendActivityAsync(CreateTypingActivity(turnContext), cancellationToken);
                await Task.Delay(200);
                await turnContext.SendActivityAsync(MessageFactory.Text($"Sent a request to John Ingram, manager of {newJoiner}, to approve onboarding"), cancellationToken);
            }
            else if (text.StartsWith("closeticket"))
            {
                await turnContext.SendActivityAsync(CreateTypingActivity(turnContext), cancellationToken);

                await Task.Delay(300);
                await turnContext.SendActivityAsync(MessageFactory.Text("Ticket closed 👍"), cancellationToken);
            }
            else if (!string.IsNullOrEmpty(text))
            {
                const string prefix = "running instruction '";
                var fromRunButton = false;
                if (text.StartsWith(prefix))
                {
                    fromRunButton = true;
                    text = text.Replace(prefix, "");
                    text = text.Split('\'')[0];
                }

                _selectedDevice ??= _lastDeviceQueried;
                if (_selectedDevice == null)
                {
                    return;
                }

                await turnContext.SendActivityAsync(CreateTypingActivity(turnContext), cancellationToken);

                InstructionHint instruction = null;
                instruction = _instructionDefs.Search(text).FirstOrDefault();

                if (instruction == null)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Sorry, no matching instruction found ☹"), cancellationToken);
                    return;
                }

                if (!fromRunButton)
                {
                    await turnContext.SendActivityAsync(
                        MessageFactory.Text($"Running instruction '{instruction.ReadableName}'..."), cancellationToken);
                }

                IEnumerable<Response> responses;

                await turnContext.SendActivityAsync(CreateTypingActivity(turnContext), cancellationToken);

                try
                {
                    responses = RunInstruction(instruction.Id, _selectedDevice);
                }
                catch (Exception e)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Exception while running instruction: {e}"), cancellationToken);
                    return;
                }
                

                var card = ToInstructionResultCard(responses);

                await turnContext.SendActivityAsync(MessageFactory.Attachment(card), cancellationToken);

                await Task.Delay(2000);
                var actionCard = ConnectorController.CreateSelectedTicketActionCard();
                if (actionCard != null)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Attachment(actionCard), cancellationToken);
                }
            }
        }

        private static Attachment CreateUserScheduleCard()
        {
            var card = new AdaptiveCard(new AdaptiveSchemaVersion(1, 0))
            {
                Body = new List<AdaptiveElement>
                {
                    new AdaptiveContainer
                    {
                        Items = new List<AdaptiveElement>
                        {
                            new AdaptiveTextBlock("From **1:30PM** to **2:30PM**"),
                            new AdaptiveTextBlock("From **5:00PM** to **4:30PM**"),
                        }
                    }
                },
                Actions = new List<AdaptiveAction>
                {
                    new AdaptiveSubmitAction
                    {
                        Title = "Schedule a Meeting",
                    },
                }
            };

            var attachment = new Attachment
            {
                ContentType = AdaptiveCard.ContentType,
                Content = card,
            };
            return attachment;
        }

        protected override Task OnTeamsMessagingExtensionCardButtonClickedAsync(ITurnContext<IInvokeActivity> turnContext, JObject cardData,
            CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        protected override Task<MessagingExtensionActionResponse> OnTeamsMessagingExtensionSubmitActionDispatchAsync(ITurnContext<IInvokeActivity> turnContext, MessagingExtensionAction action,
            CancellationToken cancellationToken)
        {
            return base.OnTeamsMessagingExtensionSubmitActionDispatchAsync(turnContext, action, cancellationToken);
        }

        protected override Task<MessagingExtensionActionResponse> OnTeamsMessagingExtensionSubmitActionAsync(ITurnContext<IInvokeActivity> turnContext, MessagingExtensionAction action,
            CancellationToken cancellationToken)
        {
            return base.OnTeamsMessagingExtensionSubmitActionAsync(turnContext, action, cancellationToken);
        }


        protected override Task<MessagingExtensionResponse> OnTeamsMessagingExtensionQueryAsync(ITurnContext<IInvokeActivity> turnContext, MessagingExtensionQuery query, CancellationToken cancellationToken)
        {
            var searchString = "";
            if (query.Parameters!= null && query.Parameters.Count > 0)
            {
                var instructionParam = query.Parameters.FirstOrDefault(p => p.Name.ToLowerInvariant() == "instruction");
                if (instructionParam != null)
                {
                    searchString = instructionParam.Value as string;
                }
                else
                {
                    searchString = "what";
                }
            }

            _lastReturnedInstructions = _instructionDefs.Search(searchString);

            var result = new MessagingExtensionResponse
            {
                ComposeExtension = new MessagingExtensionResult
                {
                    
                    AttachmentLayout = "list",
                    Type = "result",
                    Attachments = _lastReturnedInstructions.Select(ToAttachment).ToList()
                },
            };
            return Task.FromResult(result);

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

        private IEnumerable<Response> RunInstruction(int instructionId, string deviceFqdn)
        {

            var instructions = new Instructions(_transportProxy, _logProxy);
            var instruction = TachyonSdkExtensions.MakeTachyonCall(() =>
                instructions.SendTargetedInstruction(instructionId, null, 10, 10, new List<string> {deviceFqdn}));

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
            IEnumerable<Response> responseObjs = null;
            while (elapsed < maxWait)
            {
                var responseContainer =
                    TachyonSdkExtensions.MakeTachyonCall(() => responses.GetAggregatedResponses(instruction.Id, null, 15));

                if (responseContainer.Responses?.Count() > 0)
                {
                    responseObjs = responseContainer.Responses;
                    break;
                }

                elapsed = DateTime.Now - startTime;
            }

            return responseObjs;
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
                                            new AdaptiveTextBlock(device.IsActive() ? "Is online" : "Is offline")
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
                Content = card,
                
            };
        }

        private Attachment ToInstructionResultCard(IEnumerable<Response> responses)
        {
            AdaptiveCard card = null;
            if (responses == null || !responses.Any())
            {
                card = new AdaptiveCard(new AdaptiveSchemaVersion(1, 0))
                {
                    Body = new List<AdaptiveElement>
                    {
                        new AdaptiveContainer
                        {
                            Items = new List<AdaptiveElement>
                            {
                                new AdaptiveTextBlock("Instruction returned no results")
                            }
                        }
                    }
                };
            }
            else
            {
                var columnSet = new AdaptiveColumnSet();

                var columnNames = responses.First().Values.Keys.Select(kvp => kvp);

                columnSet.Columns = columnNames.Select(c => new AdaptiveColumn
                {
                    Width = "auto",
                    VerticalContentAlignment = AdaptiveVerticalContentAlignment.Center,
                    Items = new List<AdaptiveElement>()
                    {
                        new AdaptiveTextBlock(c)
                        {
                            Weight = AdaptiveTextWeight.Bolder
                        },
                    }
                }).ToList();

                foreach (var response in responses)
                {
                    for (var i = 0; i < columnSet.Columns.Count; i++)
                    {
                        var valueList = response.Values.Values.ToList();
                        columnSet.Columns[i].Items.Add(new AdaptiveTextBlock(valueList[i].ToString()));
                    }
                }

                card = new AdaptiveCard(new AdaptiveSchemaVersion(1, 0))
                {
                    Body = new List<AdaptiveElement>
                    {
                        new AdaptiveContainer
                        {
                            Items = new List<AdaptiveElement>
                            {
                                new AdaptiveTextBlock("Instruction Result")
                                {
                                    Weight = AdaptiveTextWeight.Bolder,
                                    Size = AdaptiveTextSize.Large
                                },
                                columnSet
                            }
                        },
                    }
                };
            }


            return new Attachment
            {
                ContentType = AdaptiveCard.ContentType,
                Content = card,
            };
        }
        private Attachment CreateConnectivityReportCard(string deviceName)
        {
            var card = new AdaptiveCard(new AdaptiveSchemaVersion(1, 0))
            {
                Body = new List<AdaptiveElement>
                {
                    new AdaptiveContainer
                    {
                        Items = new List<AdaptiveElement>
                        {
                            new AdaptiveTextBlock($"Connectivity report for {deviceName}")
                            {
                                Weight = AdaptiveTextWeight.Bolder, 
                                Size = AdaptiveTextSize.Large
                            },
                            new AdaptiveFactSet
                            {
                                Facts = new List<AdaptiveFact>
                                {
                                    new AdaptiveFact("Internet (8.8.8.8):", "4ms RTT"),
                                    new AdaptiveFact("Confluence:", "112ms RTT"),
                                    new AdaptiveFact("TFS:", "105ms RTT"),
                                    new AdaptiveFact("UK Gateway:", "96ms RTT"),
                                    new AdaptiveFact("UK VPN NAS:", "101ms RTT"),
                                    new AdaptiveFact("India Gateway:", "7ms RTT"),
                                    new AdaptiveFact("India VPN NAS:", "Unreachable"),
                                }
                            },
                            new AdaptiveTextBlock("Network interfaces:")
                            {
                                Spacing =  AdaptiveSpacing.Large,
                                Separator = true,
                            },
                            new AdaptiveFactSet
                            {
                                Facts = new List<AdaptiveFact>
                                {
                                    new AdaptiveFact("Ethernet", "--, Physical, Ethernet, Disconnected"),
                                    new AdaptiveFact("WiFi", "10.1.1.13, Physical, Wifi, Connected"),
                                    new AdaptiveFact("Tunnel adapter Microsoft IP-HTTPS", "10.1.1.13, Physical, Wifi, Disconnected")
                                }
                            }
                        }
                    },
                }
            };


            return new Attachment
            {
                ContentType = AdaptiveCard.ContentType,
                Content = card,
                
            };
        }

        private Attachment CreateVpnHealthCard()
        {
            var card = new AdaptiveCard(new AdaptiveSchemaVersion(1, 0))
            {
                Body = new List<AdaptiveElement>
                {
                    new AdaptiveContainer
                    {
                        Items = new List<AdaptiveElement>
                        {
                            new AdaptiveTextBlock("VPN Server Health Report")
                            {
                                Weight = AdaptiveTextWeight.Bolder, 
                                Size = AdaptiveTextSize.Large
                            },
                            new AdaptiveFactSet
                            {
                                Facts = new List<AdaptiveFact>
                                {
                                    new AdaptiveFact("CPU", "83%"),
                                    new AdaptiveFact("Memory:", "58%"),
                                    new AdaptiveFact("Network (Send):", "8.1 Gbps"),
                                    new AdaptiveFact("Network (Receive):", "759.3 Mbps"),
                                }
                            },
                        }
                    },
                }
            };


            return new Attachment
            {
                ContentType = AdaptiveCard.ContentType,
                Content = card,
                
            };
        }
        private Attachment CreateTicketCloseCard()
        {
            var card = new AdaptiveCard(new AdaptiveSchemaVersion(1, 0))
            {
                Body = new List<AdaptiveElement>
                {
                    new AdaptiveContainer
                    {
                        Items = new List<AdaptiveElement>
                        {
                            new AdaptiveTextBlock("Do you want to close the ticket?")
                            {
                            },
                        }
                    },
                },
                Actions = new List<AdaptiveAction>
                {
                    new AdaptiveSubmitAction
                    {
                        Title = "Yes, close it",
                        Data =  new AdaptiveCardAction
                        {
                            MsteamsCardAction = new CardAction
                            {
                                Type = Constants.MessageBackActionType,
                                Text = $"closeticket",
                            }
                        },
                    },
                }
            };


            return new Attachment
            {
                ContentType = AdaptiveCard.ContentType,
                Content = card,
                
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


        

        private static MessagingExtensionAttachment ToAttachment(InstructionHint instructionDefinition)
        {
            var preview = new ThumbnailCard
            {
                Title = instructionDefinition.ReadableName,
                Text = instructionDefinition.Description,
            };

            var content = new AdaptiveCard(new AdaptiveSchemaVersion(1, 0))
            {
                Body = new List<AdaptiveElement>
                {
                    new AdaptiveContainer
                    {
                        Items = new List<AdaptiveElement>
                        {
                            new AdaptiveTextBlock(instructionDefinition.ReadableName)
                            {
                                Weight = AdaptiveTextWeight.Bolder,
                                Size = AdaptiveTextSize.Large
                            },
                            new AdaptiveTextBlock(instructionDefinition.Description),
                        },
                    },
                },
                Actions = new List<AdaptiveAction>
                {
                    new AdaptiveSubmitAction
                    {
                        Title = "Run",
                        Data =  new AdaptiveCardAction
                        {
                            MsteamsCardAction = new CardAction
                            {
                                Type = "imBack",
                                Text = $"Running instruction '{instructionDefinition.ReadableName}'...",
                                Value = $"Running instruction '{instructionDefinition.ReadableName}'...",
                            }
                        },
                    },
                }
            };
            
            return new MessagingExtensionAttachment
            {
                ContentType = AdaptiveCard.ContentType,
                Content = content,
                Preview = preview.ToAttachment(),
            };
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

        public static Activity CreateTypingActivity(ITurnContext<IMessageActivity> turnContext)
        {
            var activity = turnContext.Activity as Activity;
            var reply = activity.CreateReply();
            reply.Type = ActivityTypes.Typing;
            return reply;
        }
    }
}
