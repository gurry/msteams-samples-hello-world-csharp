using System;
using System.Collections.Generic;
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
            var card = new AdaptiveCard(new AdaptiveSchemaVersion(1, 0))
            {
                Body = new List<AdaptiveElement>
                {
                    new AdaptiveContainer
                    {
                        Items = new List<AdaptiveElement>
                        {
                            new AdaptiveTextBlock(ticket.Title)
                            {
                                Weight = AdaptiveTextWeight.Bolder,
                                Size = AdaptiveTextSize.Large
                            },
                            new AdaptiveTextBlock(ticket.Message),
                            new AdaptiveFactSet()
                            {

                                Facts = new List<AdaptiveFact>
                                {
                                    new AdaptiveFact
                                    {
                                        Title = "Device",
                                        Value = ticket.Device
                                    },
                                    new AdaptiveFact
                                    {
                                        Title = "User",
                                        Value = ticket.User
                                    }
                                }
                            },
                        }
                    }
                },
                Actions = new List<AdaptiveAction>
                {
                    new AdaptiveSubmitAction
                    {
                        Title = "Investigate",
                        Data =  new AdaptiveCardAction
                        {
                            MsteamsCardAction = new CardAction
                            {
                                Type = Constants.MessageBackActionType,
                                Text = $"info {ticket.Device}",
                            },
                        },
                    },
                    new AdaptiveSubmitAction
                    {
                        Title = "Check User availability",
                        Data =  new AdaptiveCardAction
                        {
                            MsteamsCardAction = new CardAction
                            {
                                Type = Constants.MessageBackActionType,
                                Text = $"availability",
                            },
                        },
                    },
                    new AdaptiveSubmitAction
                    {
                        Title = "Call user"
                    },
                }

            };
            var attachment = new Attachment
            {
                ContentType = AdaptiveCard.ContentType,
                Content = card,
                
            };

            reply.Attachments.Clear();
            reply.Attachments.Add(attachment);

            await _connectorClient.Conversations.SendToConversationAsync((Activity)reply);

            return Ok();
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