using ErickExample.Entities;

namespace ErickExample.Interfaces
{
    interface ILoginController
    {
        Response Login(string identity, string password);

        bool CheckIdentity(string identity);
    }
}
