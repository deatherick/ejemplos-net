using MvcBusinessLogic.Interfaces;
using MvcDataAccess.Entities;

namespace MvcBusinessLogic
{
    public class LoginController
    {
        private readonly ILoginService _loginService;
        private readonly ISecurity _security;
        public LoginController(ILoginService loginService, ISecurity security)
        {
            _loginService = loginService;
            _security = security;
        }

        public Response Login(string pIdentity, string pPassword)
        {
            pPassword = _security.sha256_hash(pPassword);
            return _loginService.Login(pIdentity, pPassword);
        }
    }
}
