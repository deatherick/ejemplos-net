using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using MiBot.Dialogs;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace MiBot.Controllers
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity.Type == ActivityTypes.Message)
            {
                await Conversation.SendAsync(activity, () => new MiBotLuisDialog());
            }
            else
            {
                await HandleSystemMessage(activity);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        private static async Task<Activity> HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
                var replyMessage = string.Empty;
                replyMessage += $"Hola {message.MembersAdded.Last().Name}\n\n";
                replyMessage += "Me llamo CAEXBot. Diseñado para responder las preguntas acerca de Cargo Expreso.  \n";
                replyMessage += "Actualmente tengo las siguientes características:  \n";
                replyMessage += GreetingsDialog.HelpText;
                replyMessage += "* Pideme 'Ayuda' si necesitas que te repita estas opciones\n\n";
                replyMessage += "En el futuro tendré más opciones.";
                var connector = new ConnectorClient(new Uri(message.ServiceUrl));
                var reply = message.CreateReply(replyMessage);
                await connector.Conversations.ReplyToActivityAsync(reply);
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }
    }
}