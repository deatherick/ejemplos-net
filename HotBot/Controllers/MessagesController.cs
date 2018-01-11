using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using HotBot.Dialogs;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace HotBot.Controllers
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        private readonly string [] _nombres = {"Dayana", "Zafiro", "Ruby", "Esmeralda", "Gaby", "Estrella", "Katrina"};

        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity.Type == ActivityTypes.Message)
            {
                await Conversation.SendAsync(activity, () => new HotBotRootDialog());
            }
            else
            {
                await HandleSystemMessage(activity);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        private async Task<Activity> HandleSystemMessage(Activity message)
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
                var random = new Random();
                var index = random.Next(_nombres.Length);
                var nombre = _nombres[index];
                var replyMessage = string.Empty;
                replyMessage += $"Hola {message.MembersAdded.Last().Name}\n\n";
                replyMessage += $"Me llamo {nombre}. Estoy aquí para satisfacer todas tus lujurias.  \n";
                replyMessage += "Ahorita mis jefes solo me permiten hacer esto...  \n";
                replyMessage += GreetingsDialog.HelpText;
                replyMessage += "Puedes pedirme lo que sea soy muy complaciente:  \n";
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