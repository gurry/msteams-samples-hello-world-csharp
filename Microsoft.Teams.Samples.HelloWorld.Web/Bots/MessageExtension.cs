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
using AdaptiveCards;
using Bogus;

namespace Microsoft.Teams.Samples.HelloWorld.Web
{
    public class MessageExtension : TeamsActivityHandler
    {
        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            turnContext.Activity.RemoveRecipientMention();
            var text = turnContext.Activity.Text.Trim().ToLower();


            if (text.StartsWith("device "))
            {
                var parts = text.Split();
                if (parts.Length != 2)
                {
                    var replyText = "Wrong syntax. Try \"device <device FQDN>\"";
                    await turnContext.SendActivityAsync(MessageFactory.Text(replyText), cancellationToken);
                    return;
                }

                var fqdn = parts[1];
                await turnContext.SendActivityAsync(MessageFactory.Text($"Here's the information about {fqdn} from Tachyon:"), cancellationToken);
                var attachment = CreateDeviceCard(fqdn);
                await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment), cancellationToken);
            }
            else if (text.StartsWith("inst "))
            {
                var parts = text.Split();
                if (parts.Length != 2)
                {
                    var replyText = "Wrong syntax. Try \"inst <instruction text>\"";
                    await turnContext.SendActivityAsync(MessageFactory.Text(replyText), cancellationToken);
                    return;
                }

                await turnContext.SendActivityAsync(MessageFactory.Text("Instruction result:"), cancellationToken);
            }
        }


        private Attachment CreateDeviceCard(string fqdn)
        {
            string cardJson =
                "{\r\n  \"$schema\": \"http://adaptivecards.io/schemas/adaptive-card.json\",\r\n  \"type\": \"AdaptiveCard\",\r\n  \"version\": \"1.0\",\r\n  \"body\": [\r\n        {\r\n      \"type\": \"Container\",\r\n      \"items\": [\r\n        {\r\n          \"type\": \"TextBlock\",\r\n          \"text\": \"Client2.ntl.local\",\r\n          \"weight\": \"bolder\",\r\n          \"size\": \"large\"\r\n        },\r\n        {\r\n          \"type\": \"ColumnSet\",\r\n          \"columns\": [\r\n              {\r\n                \"type\": \"Column\",\r\n                \"width\": \"auto\",\r\n                \"items\": [\r\n                  {\r\n                    \"type\": \"TextBlock\",\r\n                    \"text\": \"•\",\r\n                    \"size\": \"extralarge\",\r\n                    \"color\": \"good\"\r\n                  }\r\n                ],\r\n                \"verticalContentAlignment\": \"Center\"\r\n              },\r\n              {\r\n                \"type\": \"Column\",\r\n                \"width\": \"auto\",\r\n                \"items\": [\r\n                  {\r\n                    \"type\": \"TextBlock\",\r\n                    \"text\": \"Currently Online\",\r\n                    \"size\": \"small\",\r\n                    \"isSubtle\": true\r\n                  }\r\n                ],\r\n                \"verticalContentAlignment\": \"Center\"\r\n              }\r\n            ]\r\n          \r\n         \r\n        },\r\n        {\r\n          \"type\": \"FactSet\",\r\n          \"facts\": [\r\n            {\r\n              \"title\": \"Name:\",\r\n              \"value\": \"Client2\"\r\n            },\r\n            {\r\n              \"title\": \"FQDN:\",\r\n              \"value\": \"Client2.ntl.local\"\r\n            },\r\n            {\r\n              \"title\": \"IP Address:\",\r\n              \"value\": \"10.1.10.20\"\r\n            },\r\n            {\r\n              \"title\": \"OS:\",\r\n              \"value\": \"Windows 10\"\r\n            },\r\n            {\r\n              \"title\": \"Device Type:\",\r\n              \"value\": \"Desktop\"\r\n            },\r\n            {\r\n              \"title\": \"Manufacturer:\",\r\n              \"value\": \"Dell Inc.\"\r\n            },\r\n            {\r\n              \"title\": \"Model:\",\r\n              \"value\": \"Precision T5600\"\r\n            },\r\n            {\r\n              \"title\": \"Serial Number:\",\r\n              \"value\": \"3C17L72\"\r\n            },\r\n            {\r\n              \"title\": \"Domain:\",\r\n              \"value\": \"NTL.local\"\r\n            },\r\n            {\r\n              \"title\": \"Location:\",\r\n              \"value\": \"Noida\"\r\n            },\r\n            {\r\n              \"title\": \"Last Boot Time (UTC):\",\r\n              \"value\": \"2019-02-04T00:00:00Z\"\r\n            }\r\n          ]\r\n        }\r\n      ]\r\n    }  \r\n  ]\r\n}\r\n";

            var parsedResult = AdaptiveCard.FromJson(cardJson);
            return new Attachment
            {
                ContentType = AdaptiveCard.ContentType,
                Content = parsedResult.Card,
            };
        }


        protected override Task<MessagingExtensionResponse> OnTeamsMessagingExtensionQueryAsync(ITurnContext<IInvokeActivity> turnContext, MessagingExtensionQuery query, CancellationToken cancellationToken)
        {
            var title = "";
            var titleParam = query.Parameters?.FirstOrDefault(p => p.Name == "cardTitle");
            if (titleParam != null)
            {
                title = titleParam.Value.ToString();
            }

            if (query == null || query.CommandId != "getRandomText")
            {
                // We only process the 'getRandomText' queries with this message extension
                throw new NotImplementedException($"Invalid CommandId: {query.CommandId}");
            }

            var attachments = new MessagingExtensionAttachment[5];

            for (int i = 0; i < 5; i++)
            {
                attachments[i] = GetAttachment(title);
            }

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
                Images = new List<CardImage> { new CardImage("http://lorempixel.com/640/480?rand=" + DateTime.Now.Ticks.ToString()) }
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
    }
}
