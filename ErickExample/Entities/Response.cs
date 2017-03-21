namespace ErickExample.Entities
{
    internal class Response
    {
        public enum ResponseCode
        {
            Success = 0,
            LoginError = -1
        }

        public ResponseCode Code { get; private set; }

        public bool Success { get; private set; }

        public string Message { get; private set; }

        public object Object { get; private set; }

        public Response(ResponseCode code, bool success, string message, object pObject = null)
        {
            Code = code;
            Success = success;
            Message = message;
            Object = pObject;
        }

        public Response(ResponseCode code, bool success)
        {
            Code = code;
            Success = success;
        }
    }
}
