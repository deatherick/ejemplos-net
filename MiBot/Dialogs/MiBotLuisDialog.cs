using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MiBot.Entities;
using MiBot.Internal;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;

namespace MiBot.Dialogs
{
    [Serializable]
    [LuisModel("c31afac2-1f7c-4edd-89a0-c5f04e28adff", "ee7d37c7e74c4572bab0b412c803132c")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class MiBotLuisDialog : LuisDialog<object>
    {
        private const string BLOG_BASE_URL = "https://ankitbko.github.io";
        private const string CAEX_URL = "https://www.cargoexpreso.com";

        #region Intents

        [LuisIntent("None")]
        [LuisIntent("")]
        public async Task None(IDialogContext context, IAwaitable<IMessageActivity> message, LuisResult result)
        {
            var cts = new CancellationTokenSource();
            await context.Forward(new GreetingsDialog(), GreetingDialogComplete, await message, cts.Token);
        }

        [LuisIntent("SobreMi")]
        public async Task SobreMi(IDialogContext context, LuisResult result)
        {
            await context.PostAsync(@"Por más de 30 años CARGO EXPRESO se ha dedicado a cubrir eficientemente las necesidades de manejo de documentos, paquetes y logística en general de importantes empresas en la región, el fruto de esta labor es la confianza que nuestros clientes depositan en nosotros.");
            await context.PostAsync(@"El entender su negocio y la importancia de cumplir en brindar un servicio de excelente calidad, garantizando recolección y entrega en el lugar y hora que el cliente necesita, es la forma en que pagamos esta confianza.");
            context.Wait(MessageReceived);
        }

        [LuisIntent("Busqueda")]
        public async Task Busqueda(IDialogContext context, LuisResult result)
        {
            var tag = string.Empty;
            var posts = new List<Post>();

            try
            {
                if (result.Entities.Count > 0)
                {
                    tag = result.Entities.FirstOrDefault(e => e.Type == "Tag")?.Entity;
                }

                if (!string.IsNullOrWhiteSpace(tag))
                {
                    var bs = new BlogSearch();
                    posts = bs.GetPostsWithTag(tag);
                }

                var replyText = GenerateResponseForBlogSearch(posts, tag);
                await context.PostAsync(replyText);
            }
            catch (Exception)
            {
                await context.PostAsync("Algo realmente malo sucedió. Puedes intentarlo más tarde, mientras tanto, verificaremos qué salió mal.");
            }
            finally
            {
                context.Wait(MessageReceived);
            }
        }

        [LuisIntent("Guias")]
        public async Task Guias(IDialogContext context, LuisResult result)
        {
            try
            {
                await context.PostAsync("Perfecto, a continuación te solicitaré unos datos para que localicemos tu paquete.");
                var guiaForm = new FormDialog<GuiaForm>(new GuiaForm(), GuiaForm.BuildForm, FormOptions.PromptInStart);
                context.Call(guiaForm, GuiaFormComplete);
            }
            catch (Exception)
            {
                await context.PostAsync("Algo realmente malo sucedió. Puedes intentarlo más tarde, mientras tanto, verificaremos qué salió mal.");
                context.Wait(MessageReceived);
            }
        }

        [LuisIntent("Recolecta")]
        public async Task Recolecta(IDialogContext context, LuisResult result)
        {
            try
            {
                await context.PostAsync("Perfecto, a continuación te solicitaré unos datos para que programemos una recolección.");
                var guiaForm = new FormDialog<RecolectaForm>(new RecolectaForm(), RecolectaForm.BuildForm, FormOptions.PromptInStart);
                context.Call(guiaForm, RecolectaFormComplete);
            }
            catch (Exception)
            {
                await context.PostAsync("Algo realmente malo sucedió. Puedes intentarlo más tarde, mientras tanto, verificaremos qué salió mal.");
                context.Wait(MessageReceived);
            }
        }

        #endregion

        #region Funciones

        private static string GenerateResponseForBlogSearch(IReadOnlyCollection<Post> posts, string tag)
        {
            if (string.IsNullOrWhiteSpace(tag))
                return "No entendí qué tema estás buscando. Puede ser que CAEX no haya escrito ningún artículo, así que no puedo reconocer ese tema. Puede tratar de cambiar el tema e intentar nuevamente.";
            if (posts.Count == 0)
                return "CAEX no ha escrito ningún artículo sobre " + tag + ". Contactanos en Twitter para hacernos saber que estas interesado en " + tag;

            var replyMessage = string.Empty;
            replyMessage += $"Tengo {posts.Count} articulos sobre {tag} \n\n";
            replyMessage = posts.Aggregate(replyMessage, (current, post) => current + $"* [{post.Name}]({BLOG_BASE_URL}{post.Url})\n\n");
            replyMessage += "Diviértete leyendo. Publica un comentario si te gustan.";
            return replyMessage;
        }

        #endregion

        #region Dialogos Completados

        private async Task GreetingDialogComplete(IDialogContext context, IAwaitable<object> result)
        {
            var success = await result;
            if (!(bool)success)
                await context.PostAsync("Lo siento, no te he entendido.");

            context.Wait(MessageReceived);
        }

        private async Task GuiaFormComplete(IDialogContext context, IAwaitable<GuiaForm> result)
        {
            try
            {
                var thumbnail = new HeroCard();
                var feedback = await result;
                thumbnail.Title = "Hemos encontrado la información de tu Guia";
                thumbnail.Images = new[] { new CardImage("https://www.cargoexpreso.com/wp-content/uploads/2017/01/logo-4.png") };
                thumbnail.Subtitle = $"*Guía:* {feedback.Guia.NumeroGuia}";
                var replyMessage = string.Empty;
                //replyMessage += "Hemos encontrado la información de tu Guia: \n\n";
                //replyMessage += $"*Numero de guía:* {feedback.Guia.NumeroGuia} \n\n";
                replyMessage += "*Estado:* Entregado \n\n";
                replyMessage += $"*Remitente:* {feedback.Guia.RemitenteNombre} \n\n";
                replyMessage += $"*Destinatario:* {feedback.Guia.DestinatarioNombre} \n\n";
                replyMessage += $"*Acuse de recibido:* {feedback.Guia.PodNombre} {feedback.Guia.PodFecha} \n\n";
                thumbnail.Text = replyMessage;
                thumbnail.Buttons = new[] {new CardAction(ActionTypes.OpenUrl, "Más Información", value: $"{CAEX_URL}/tracking/?guia={feedback.Guia.NumeroGuia}")};
                var reply = context.MakeMessage();
                reply.Attachments.Add(thumbnail.ToAttachment());
                reply.Attachments.Add(thumbnail.ToAttachment());
                //replyMessage += $"Puedes encontrar más información aquí: [Más Información]({CAEX_URL}/tracking/?guia={feedback.Guia.NumeroGuia})\n\n";
                await context.PostAsync(reply);
                await context.PostAsync($"¿Hay algo más en lo que pueda ayudarte {feedback.Nombre}?");
                //context.Wait(MessageReceivedAsync);
            }
            catch (FormCanceledException)
            {
                await context.PostAsync("¿Hay algo más en lo que pueda ayudarte?");
            }
            catch (Exception)
            {
                await context.PostAsync("Algo realmente malo sucedió. Puedes intentarlo más tarde, mientras tanto, verificaremos qué salió mal.");
            }
            finally
            {
                context.Wait(MessageReceived);
            }
        }

        private async Task RecolectaFormComplete(IDialogContext context, IAwaitable<RecolectaForm> result)
        {
            try
            {
                var feedback = await result;
                var receipt = new ReceiptCard
                {
                    Title = $"Referencia: *{feedback.IdRecolecta}*\n\n",
                    Total = "Q275.00",
                    Tax = "IVA Q25.00",
                    Items = new List<ReceiptItem>(),
                    Buttons = new [] { new CardAction(ActionTypes.OpenUrl, "Ver Factura", value: $"{CAEX_URL}/tracking/?invoice={feedback.IdRecolecta}")}
                };
                for (var i = 0; i < 2; i++)
                {
                    var item = new ReceiptItem
                    {
                        Title = feedback.TipoDeEnvio.ToString(),
                        Subtitle = feedback.FechaDeRecolecta.ToString("hh tt", CultureInfo.InvariantCulture),
                        Image = new CardImage("https://www.cargoexpreso.com/wp-content/uploads/2017/01/logo-4.png"),
                        Price = "125.00",
                        Quantity = "1",
                        Text = "Por servicio de recolecta",
                    };
                    receipt.Items.Add(item);
                }
               
                
                var reply = context.MakeMessage();
                reply.Attachments.Add(receipt.ToAttachment());
                await context.PostAsync(reply);
                await context.PostAsync($"¿Hay algo más en lo que pueda ayudarte {feedback.Nombre}?");

            }
            catch (FormCanceledException)
            {
                await context.PostAsync("¿Hay algo más en lo que pueda ayudarte?");
            }
            catch (Exception)
            {
                await context.PostAsync("Algo realmente malo sucedió. Puedes intentarlo más tarde, mientras tanto, verificaremos qué salió mal.");
            }
            finally
            {
                context.Wait(MessageReceived);
            }
        }

        #endregion

    }
}