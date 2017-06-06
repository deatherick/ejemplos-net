using MvcDataAccess.Entities;

namespace MvcBusinessLogic.Interfaces
{
    public interface ILoginService
    {
        Response Login(string identity, string password);

        bool CheckIdentity(string identity);
    }
}
