using Application.Interfaces.ApiClients;
using Domain.DTOs.Award;

namespace Application.Interfaces.RepositoryApi
{
    public interface IRepositoryUserAward
    {
        Task PostUserAward(Guid idAward,IUserScopedApiClient userScopedApiClient);
        Task<ICollection<ReturnAwardDTO>> GetUserAward(IUserScopedApiClient userScopedApiClient);
    }
}
