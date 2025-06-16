using Application.Interfaces.ApiClients;
using Domain.DTOs.User;

namespace Application.Session
{
    public interface ISessionService:ISessionLenguage, ISessionState
    {
        IUserScopedApiClient GetApi(UserDTO user);
    }
}
