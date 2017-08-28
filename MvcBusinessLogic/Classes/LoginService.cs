using System;
using MvcBusinessLogic.Interfaces;
using MvcDataAccess.Entities;

namespace MvcBusinessLogic.Classes
{
    internal class LoginService : ILoginService
    {

        public Response Login(string identity, string password)
        {
            if (!CheckIdentity(identity))
                return new Response(Response.ResponseCode.LoginError, false, "Las credenciales no coinciden");
            //sha256 a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3 = 123
            if (identity == "erick" && password == "a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3")
            {
                return new Response(Response.ResponseCode.Success, true, "Usuario logueado", new Person(1, "Erick Morales", DateTime.Today));
            }
            return new Response(Response.ResponseCode.LoginError, false, "Las credenciales no coinciden");
        }

        public bool CheckIdentity(string identity)
        {
            return true;
        }

    }

    internal class LoginServiceDuplicate : ILoginService
    {

        public Response Login(string identity, string password)
        {
            return new Response(Response.ResponseCode.Success, true, "Usuario logueado", new Person(1, "Erick Morales", DateTime.Today));
        }

        public bool CheckIdentity(string identity)
        {
            return true;
        }
    }
}
