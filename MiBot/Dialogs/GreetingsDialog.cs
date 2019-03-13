using System;
using System.Threading.Tasks;
using BestMatchDialog;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;

namespace MiBot.Dialogs
{
    [Serializable]
    [LuisModel("c31afac2-1f7c-4edd-89a0-c5f04e28adff", "ee7d37c7e74c4572bab0b412c803132c")]
    public class GreetingsDialog : BestMatchDialog<object>
    {
        [BestMatch(new[] { "Hola", "Buen dia", "Buen día", "Oye", "Buenos dias", "Que tal", "Saludos", "Buena mañana", "Buenas tardes", "Buenos días", "Buenas noches" })]
        public async Task WelcomeGreeting(IDialogContext context, string messageText)
        {
            await context.PostAsync("Hola. ¿Cómo puedo ayudarte?");
            context.Done(true);
        }

        [BestMatch(new[] { "Adios", "Bye", "Feliz noche", "Feliz tarde", "Feliz día", "Feliz dia" })]
        public async Task FarewellGreeting(IDialogContext context, string messageText)
        {
            await context.PostAsync("Adios, ten un buen día.");
            context.Done(true);
        }

        [BestMatch(new[] { "Ayuda", "Help" })]
        public async Task HelpAction(IDialogContext context, string messageText)
        {
            var replyMessage = string.Empty;
            replyMessage += "Puedes probar con lo siguiente:  \n";
            replyMessage += HelpText;
            await context.PostAsync(replyMessage);
            context.Done(true);
        }

        public override async Task NoMatchHandler(IDialogContext context, string messageText)
        {
            await context.PostAsync("No Match Handler");
            context.Done(false);
        }

        public static string HelpText
        {
            get
            {
                var replyMessage = string.Empty;
                replyMessage += "* Preguntamente acerca de mi creador, Prueba: '¿Quienes son Cargo Expreso?'\n\n";
                replyMessage += "* Prueba buscar una guia escribiendo lo siguiente: 'Quiero rastrear un paquete, Quiero localizar mi guia'\n\n";
                replyMessage += "* Quieres solicitar una recolecta, Prueba: 'Quiero solicitar una recolecta, Solicito una recolecta'\n\n";
                return replyMessage;
            }

        }
    }
}