using Application.Interfaces.ApiClients;
using Application.Interfaces.RepositoryApi;
using Domain.DTOs.Award;

namespace Infrastructure.RepositoryApi
{
    internal class RepositoryAward : IRepositoryAward
    {
        public async Task<ICollection<ReturnAwardDTO>> GetAwards(IUserScopedApiClient userScopedApiClient)
        {
            return await userScopedApiClient.ExecuteAsUserAsync(async action =>
            {
                return await action.GetAwardsAsync();
            });
        }
    }
}
