using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;

namespace HotBot.Dialogs
{
    [Serializable]
    [LuisModel("76096828-2666-48a5-bf14-6b857670d558", "ee7d37c7e74c4572bab0b412c803132c")]
    public class HotBotRootDialog : LuisDialog<object>
    {

        [LuisIntent("None")]
        [LuisIntent("")]
        public async Task None(IDialogContext context, IAwaitable<IMessageActivity> message, LuisResult result)
        {
            var cts = new CancellationTokenSource();
            await context.Forward(new GreetingsDialog(), GreetingDialogComplete, await message, cts.Token);
        }

        #region Dialogos Completados

        private async Task GreetingDialogComplete(IDialogContext context, IAwaitable<object> result)
        {
            var success = await result;
            if (!(bool)success)
                await context.PostAsync("Lo siento papi rico, no me permiten hacer eso :$.");

            context.Wait(MessageReceived);
        }
#endregion

    }
}