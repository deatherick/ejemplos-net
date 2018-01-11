using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.FormFlow;

namespace MiBot.Dialogs
{
    public enum TiposEnvio
    {
        Sobre,
        Paquete,
        CajasNormales,
        MensajeriaRapida,
        SobresLocales
    }

    [Serializable]
    public class RecolectaForm
    {
        [Prompt("¿Posees código de crédito? (Si, No)")]
        public bool PoseeCredito { get; set; }

        [Prompt("¿Cual es tu nombre?")]
        public string Nombre { get; set; }

        [Prompt("Porfavor escribe tu código de crédito")]
        public string CodigoDeCredito { get; set; }

        [Prompt("Porfavor escribe tu dirección")]
        public string Direccion { get; set; }

        [Prompt("¿Qué estás enviando...? {||}")]
        public TiposEnvio TipoDeEnvio { get; set; }

        [Prompt("¿Cuánto pesa lo que estás enviando?")]
        [Numeric(1, int.MaxValue)]
        public int Peso { get; set; }

        [Prompt("Porfavor dinos a que hora deseas tu recolecta")]
        public DateTime FechaDeRecolecta { get; set; }

        public int IdRecolecta { get; set; } = 111197823;

        public static IForm<RecolectaForm> BuildForm()
        {
            return new FormBuilder<RecolectaForm>()
                .Field(nameof(PoseeCredito), validate: PreguntarEsCredito)
                .Field(nameof(CodigoDeCredito), active: PreguntarCodigo, validate: ValidarCodigoCredito)
                .Field(nameof(Nombre), active: PreguntarNombre)
                .Field(nameof(Direccion), active: PreguntarNombre)
                .Field(nameof(FechaDeRecolecta), validate: ValidarFechaRecolecta)
                //.Field(nameof(NumeroGuia), validate: ValidarGuia)
                .AddRemainingFields(exclude: new [] {nameof(IdRecolecta)})
                //.OnCompletion(ProcessOrder)
                .Confirm("¿Estás seguro de continuar?")
                .Build();
        }

        private static Task<ValidateResult> ValidarFechaRecolecta(RecolectaForm state, object response)
        {
            var result = new ValidateResult()
            {
                IsValid = true,
                Value = response
            };
            var fecha = (DateTime)response;
            if (fecha.Hour <= DateTime.Now.Hour)
            {
                result.IsValid = false;
                result.Feedback = "La hora que elegiste es menor a la hora actual";
            }
            if (fecha.Date.Hour >= 17 || fecha.Date.Hour < 8)
            {
                result.IsValid = false;
                result.Feedback = "Nuestros servicios de recolección trabajan de 8am a 4pm";
            }
            return Task.FromResult(result);
        }

        private static Task<ValidateResult> PreguntarEsCredito(RecolectaForm state, object response)
        {
            var result = new ValidateResult();
            if ((bool)response)
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

        private static Task<ValidateResult> ValidarCodigoCredito(RecolectaForm state, object response)
        {
            var result = new ValidateResult();
            if (((string)response).ToLower() == "na")
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

        private static bool PreguntarCodigo(RecolectaForm state) => state.PoseeCredito;

        private static bool PreguntarNombre(RecolectaForm state) => !state.PoseeCredito;
    }
}