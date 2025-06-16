using Application.Interfaces.ApiClients;
using Domain.DTOs.Award;

namespace Application.Interfaces.RepositoryApi
{
    public interface IRepositoryAward
    {
        Task<ICollection<ReturnAwardDTO>> GetAwards(IUserScopedApiClient userScopedApiClient);
    }
}
