using Application.Interfaces.ApiClients;
using Domain.DTOs.User;

namespace Application.Interfaces.RepositoryApi
{
    public interface IRepositoryUser
    {
        Task<string> CreateUser(UserDTO user);
        Task<UserDTO> GetUser(IUserScopedApiClient userScopedApiClient);
    }
}
