using Application.Interfaces.ApiClients;
using Application.Interfaces.RepositoryApi;
using Domain.DTOs.Award;

namespace Infrastructure.RepositoryApi
{
    internal class RepositoryUserAward : IRepositoryUserAward
    {
        public async Task<ICollection<ReturnAwardDTO>> GetUserAward(IUserScopedApiClient userScopedApiClient)
        {
            return await userScopedApiClient.ExecuteAsUserAsync(async action =>
            {
                return await action.GetUserAwardAsync();
            });
        }

        public async Task PostUserAward(Guid idAward, IUserScopedApiClient userScopedApiClient)
        {
            await userScopedApiClient.ExecuteAsUserAsync(async action =>
            {
                await action.PostUserAwardAsync(idAward);
            });
        }
    }
}
