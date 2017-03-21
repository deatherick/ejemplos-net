using ErickExample.Entities;
using ErickExample.Interfaces;

namespace ErickExample
{
    internal class LoginImplement
    {
        private readonly ILoginController _loginController;
        private readonly ISecurity _security;
        public LoginImplement(ILoginController loginController, ISecurity security)
        {
            _loginController = loginController;
            _security = security;
        }

        public Response Login(string pIdentity, string pPassword)
        {
            pPassword = _security.sha256_hash(pPassword);
            return _loginController.Login(pIdentity, pPassword);
        }
    }
}
