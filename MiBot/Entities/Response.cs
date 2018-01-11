namespace MiBot.Entities
{
    /// <summary>
    /// Codigos de respuesta
    /// </summary>
    public enum ResponseCode
    {
        Success = 0,
        AvisoPass = -1,
        CambiarPass = -2,
        Inactivo = -3,
        Incorrecto = -4,
        ErrorLeyendoDatos = -5,
        ErrorDeSistema = -6,
        DigitarGuia = -7,
        DigitarGuiaHija = -8,
        Reingreso = -9,
        AgregarGuia = -10,
        AgregadoReingreso = -11,
        SinDatos = -12,
        ErrorActualizandoPod = -13,
        ErrorObteniendoCliente = -14
    }

    /// <summary>
    /// Clase que maneja las respuestas recibidas por cualquier funcion
    /// </summary>
    public class Response<T>
    {
        /// <summary>
        /// Objeto que almacena el codigo de respuesta
        /// </summary>
        public ResponseCode Code { get; }

        /// <summary>
        /// Variable que indica si el resultado es el esperado
        /// </summary>
        public bool Success { get; }

        /// <summary>
        /// Mensaje de texto que devuelve la respuesta
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Objeto opcional de respuesta
        /// </summary>
        public T Object { get; }

        /// <summary>
        /// Constructor completo de objeto response
        /// </summary>
        /// <param name="code">Codigo de respuesta</param>
        /// <param name="success">Variable de control</param>
        /// <param name="message">Mensaje de respuesta</param>
        /// <param name="pObject">Objeto opcional adicional</param>
        public Response(ResponseCode code, bool success, string message, T pObject)
        {
            Code = code;
            Success = success;
            Message = message;
            Object = pObject;
        }

        /// <summary>
        /// Constructor completo de objeto response
        /// </summary>
        /// <param name="code">Codigo de respuesta</param>
        /// <param name="success">Variable de control</param>
        /// <param name="message">Mensaje de respuesta</param>
        public Response(ResponseCode code, bool success, string message)
        {
            Code = code;
            Success = success;
            Message = message;
        }

        /// <summary>
        /// Constructor simple para respuestas simples
        /// </summary>
        /// <param name="code">Codigo de respuesta</param>
        /// <param name="success">Variable de control</param>
        public Response(ResponseCode code, bool success)
        {
            Code = code;
            Success = success;
        }
    }
}