using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdaptiveCards;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Connector.Authentication;
using Microsoft.Bot.Schema;
using Microsoft.VisualBasic;

namespace Microsoft.Teams.Samples.HelloWorld.Web
{
    [Route("connector")]
    [ApiController]
    public class ConnectorController : ControllerBase
    {
        static ConnectorClient _connectorClient = new ConnectorClient(new Uri("https://smba.trafficmanager.net/in/"), new MicrosoftAppCredentials("2ea52b84-e497-4bcb-8b5d-9670b57c2915", "-a6Sd~nx~a8vXs3Z820L_t9m3JluvFVl1-"));
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Ticket ticket)
        {
            //var activity = Activity.CreateMessageActivity();
            //activity.From = new ChannelAccount("28:2ea52b84-e497-4bcb-8b5d-9670b57c2915", "SampleBot");
            //activity.Recipient = new ChannelAccount("29:1dpGDg9UIP_AePDjzUw_cL3BvV6Eofhm3cYE-HYtBVfrngPPHnlZbbpd1Ke-2498aom_Wa88JzkhLq-em3h31Ew", "Gurinder Singh");
            //activity.Conversation = new ConversationAccount(null, null, "a:1IWs1mSZBPvj3_HCA1Jqcax2DEtcAgGESi-QVF77p3fWK2pERroJfpw7awmjoGlqHDDYPfZsm6o4YPcOiFpH63-bf1fGooIINCo-siWF6HI9jTGCdayj7N4ECXqektMSI");
            //activity.ChannelId = "msteams";
            //activity.ServiceUrl = "https://smba.trafficmanager.net/in/";
            //activity.Text = "Here's a notification";

            var reply = MessageExtension._reply;


            reply.Text = "A new ticket has been assigned to you:";
            var attachment = CreateTicketCard(ticket.Title, ticket.Message, ticket.Device, ticket.User);

            reply.Attachments.Clear();
            reply.Attachments.Add(attachment);

            await _connectorClient.Conversations.SendToConversationAsync(reply);


            await Task.Delay(800);


            reply.Text = "Here are some actions to help resolve this ticket:";
            var attachment2 = CreateActionCard(new Dictionary<string, string>
            {
                { "Run Instructions", $"info {ticket.Device}" },
            }, new Dictionary<string, string>
            {
                { "Check User availability", "availability" },
                { "Call User", "" },
            });

            reply.Attachments.Clear();
            reply.Attachments.Add(attachment2);

            await _connectorClient.Conversations.SendToConversationAsync(reply);

            return Ok();
        }

        private static Attachment CreateTicketCard(string ticketTitle, string ticketDescription, string deviceName, string userName)
        {
            var card = new AdaptiveCard(new AdaptiveSchemaVersion(1, 0))
            {
                Body = new List<AdaptiveElement>
                {
                    new AdaptiveContainer
                    {
                        Items = new List<AdaptiveElement>
                        {
                            new AdaptiveTextBlock(ticketTitle)
                            {
                                Weight = AdaptiveTextWeight.Bolder,
                                Size = AdaptiveTextSize.ExtraLarge
                            },
                            new AdaptiveTextBlock(ticketDescription),
                            new AdaptiveFactSet()
                            {
                                Facts = new List<AdaptiveFact>
                                {
                                    new AdaptiveFact
                                    {
                                        Title = "Device",
                                        Value = deviceName
                                    },
                                    new AdaptiveFact
                                    {
                                        Title = "User",
                                        Value = userName
                                    }
                                }
                            },
                        }
                    }
                },
            };

            return new Attachment
            {
                ContentType = AdaptiveCard.ContentType,
                Content = card,
            };
        }

        private static Attachment CreateActionCard(IDictionary<string, string> ticketActions, IDictionary<string, string> userActions)
        {
            static AdaptiveActionSet ToAdaptiveActionSet(IDictionary<string, string> actions)
            {
                return  new AdaptiveActionSet
                {
                    Actions = actions.Select(ta => new AdaptiveSubmitAction
                    {
                        Title = ta.Key,
                        Data = new AdaptiveCardAction
                        {
                            MsteamsCardAction = new CardAction
                            {
                                Type = Constants.MessageBackActionType,
                                Text = ta.Value,
                            },
                        },
                    }).Cast<AdaptiveAction>().ToList()
                };
            }

            var ticketActionSet = ToAdaptiveActionSet(ticketActions);
            var userActionSet = ToAdaptiveActionSet(userActions);
            userActionSet.Spacing = AdaptiveSpacing.Large;

            var card = new AdaptiveCard(new AdaptiveSchemaVersion(1, 0))
            {
                Body = new List<AdaptiveElement>
                {
                    new AdaptiveContainer
                    {
                        Items = new List<AdaptiveElement>
                        {
                            ticketActionSet,
                            userActionSet
                        }
                    },
                },
            };

            return new Attachment
            {
                ContentType = AdaptiveCard.ContentType,
                Content = card,
            };
        }
    }

    public class Ticket
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string User { get; set; }
        public string Device { get; set; }
    }
}