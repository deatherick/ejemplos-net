using System;
using System.Threading.Tasks;
using MiBot.DataAccess;
using MiBot.Entities;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;

namespace MiBot.Dialogs
{
    [Serializable]
    public class GuiaForm
    {
        [Prompt("¿Posees código de crédito? (Si, No)")]
        public bool PoseeCredito { get; set; }

        [Prompt("¿Cual es tu nombre?")]
        public string Nombre { get; set; }

        [Prompt("Porfavor escribe tu código de crédito")]
        public string CodigoDeCredito { get; set; }

        [Prompt("Porfavor escribe el número de la guia")]
        public string NumeroGuia { get; set; }

        public Guia Guia { get; set; }

        public static IForm<GuiaForm> BuildForm()
        {
            return new FormBuilder<GuiaForm>()
                .Field(nameof(PoseeCredito), validate: PreguntarEsCredito)
                .Field(nameof(CodigoDeCredito), active: PreguntarCodigo, validate: ValidarCodigoCredito)
                .Field(nameof(Nombre), active: PreguntarNombre)
                .Field(nameof(NumeroGuia), validate: ValidarGuia)
                //.AddRemainingFields()
                //.OnCompletion(ProcessOrder)
                .Confirm("¿Estás seguro de continuar?")
                .Build();
        }

        private static Task<ValidateResult> ValidarGuia(GuiaForm state, object response)
        {
            var result = new ValidateResult();
            using (var guiaDa = new GuiasDa())
            {
                var resp = guiaDa.ObtenerGuia((string) response);
                if (resp.Success)
                {
                    result.IsValid = true;
                    result.Value = response;
                    state.Guia = resp.Object;
                }
                else
                {
                    result.IsValid = false;
                    result.Feedback = "No pudimos encontrar la guia en nuestro sistema, porfavor introduce denuevo tu guia o escribe *Salir* si quieres terminar la busqueda.";
                }
            }
            return Task.FromResult(result);
        }

        private static Task<ValidateResult> PreguntarEsCredito(GuiaForm state, object response)
        {
            var result = new ValidateResult();
            if ((bool) response)
            {
                result.IsValid = true;
                result.Value = true;
            }
            else
            {
                result.IsValid = true;
                result.Value = false;
            }
            //else
            //{
            //    result.IsValid = false;
            //    result.Feedback = "Lo siento no entendí tu respuesta \n\n* Respuestas:(Si, No)";
            //}
            return Task.FromResult(result);
        }

        private static Task<ValidateResult> ValidarCodigoCredito(GuiaForm state, object response)
        {
            var result = new ValidateResult();
            if (((string) response).ToLower() == "na")
            {
                result.IsValid = true;
                state.PoseeCredito = false;
            }
            else
            {
                result.IsValid = false;
                result.Feedback = "Lo siento no pudimos encontrar tu código de crédito \n\nEscribe *NA* si no recuerdas tu codigo de credito.";
            }
            
            return Task.FromResult(result);
        }

        private static Task<ValidateResult> ValidateContactInformation(GuiaForm state, object response)
        {
            var result = new ValidateResult();
            //string contactInfo = string.Empty;
            //if (GetTwitterHandle((string)response, out contactInfo) || GetEmailAddress((string)response, out contactInfo))
            //{
            //    result.IsValid = true;
            //    result.Value = contactInfo;
            //}
            //else
            //{
            //    result.IsValid = false;
            //    result.Feedback = "You did not enter valid email address or twitter handle. Make sure twitter handle starts with @.";
            //}
            result.IsValid = false;
            result.Feedback = "You did not enter valid email address or twitter handle. Make sure twitter handle starts with @.";

            return Task.FromResult(result);
        }

        private static bool PreguntarCodigo(GuiaForm state) => state.PoseeCredito;

        private static bool PreguntarNombre(GuiaForm state) => !state.PoseeCredito;

        //private static bool PreguntarGuia(GuiaForm state) =>
        //    !string.IsNullOrWhiteSpace(state.CodigoDeCredito)
        //    && !string.IsNullOrWhiteSpace(state.Nombre);

        //private static readonly OnCompletionAsyncDelegate<GuiaForm> ProcessOrder = async (context, state) =>
        //{
        //    await context.Forward(new OtherFormDialog(state), OnOtherFormProcessed, state, CancellationToken.None);
        //};

        //private static async Task OnOtherFormProcessed(IDialogContext context, IAwaitable<object> result)
        //{
        //    var formValue = await result as GuiaForm;

        //    await context.PostAsync($"You are again in the form: Time: {formValue?.Nombre}, Number1: {formValue?.NumeroGuia}, Number2: {formValue?.CodigoDeCredito}");
        //}

    }

    public class OtherFormDialog : IDialog<GuiaForm>
    {
        private readonly GuiaForm _formData;

        public OtherFormDialog(GuiaForm prevState)
        {
            _formData = prevState;
        }

        public async Task StartAsync(IDialogContext context)
        {
            //Here, we can do anything
            await context.PostAsync("Verificare tus datos. ¡Por favor espera!");

            await context.PostAsync("¡Solo bromeaba! No haré ninguna validación.");
            context.Done(_formData);
        }
    }
}