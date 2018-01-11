using System;
using System.Threading.Tasks;
using BestMatchDialog;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;

namespace HotBot.Dialogs
{
    [Serializable]
    [LuisModel("76096828-2666-48a5-bf14-6b857670d558", "ee7d37c7e74c4572bab0b412c803132c")]
    public class GreetingsDialog : BestMatchDialog<object>
    {
        [BestMatch(new[] { "Hola", "Buen dia", "Buen día", "Oye", "Buenos dias", "Que tal", "Saludos", "Buena mañana", "Buenas tardes", "Buenos días", "Buenas noches" })]
        public async Task WelcomeGreeting(IDialogContext context, string messageText)
        {
            await context.PostAsync("Hola Papi rico, quieres que hablemos cosas sucias mi rey?");
            context.Done(true);
        }

        [BestMatch(new[] { "Adios", "Bye", "Feliz noche", "Feliz tarde", "Feliz día", "Feliz dia" })]
        public async Task FarewellGreeting(IDialogContext context, string messageText)
        {
            await context.PostAsync("Adios cosita rica, espero hayas mojado tus pantalones pensando en mi.");
            context.Done(true);
        }

        [BestMatch(new[] { "Ayuda", "Help" })]
        public async Task HelpAction(IDialogContext context, string messageText)
        {
            var replyMessage = string.Empty;
            replyMessage += "Puedes hacerme lo siguiente:  \n";
            replyMessage += HelpText;
            await context.PostAsync(replyMessage);
            context.Done(true);
        }

        public override async Task NoMatchHandler(IDialogContext context, string messageText)
        {
            context.Done(false);
        }

        public static string HelpText
        {
            get
            {
                var replyMessage = string.Empty;
                replyMessage += "* Hablar de la ropa que llevo puesta\n\n";
                replyMessage += "* Pedirme fotos hot para que comencemos nuestras fantasias \n\n";
                replyMessage += "* Hablar de como voy a cumplir tu fantasia mas erotica\n\n";
                return replyMessage;
            }

        }
    }
}