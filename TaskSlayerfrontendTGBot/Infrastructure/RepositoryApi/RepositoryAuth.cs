using Application.Interfaces.ApiClients;
using Application.Interfaces.RepositoryApi;
using Domain.DTOs.User;

namespace Infrastructure.RepositoryApi
{
    internal class RepositoryAuth : IRepositoryAuth
    {
        private readonly ITaskSlayerApiClient _api;
        public RepositoryAuth(ITaskSlayerApiClient api) 
        { 
            _api = api;
        }
        public async Task<string> Authorization(UserDTO user)
        {
            return await _api.AuthorizationAsync(user);
        }
    }
}
