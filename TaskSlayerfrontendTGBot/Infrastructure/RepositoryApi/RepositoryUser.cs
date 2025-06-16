using Application.Interfaces.ApiClients;
using Application.Interfaces.RepositoryApi;
using Domain.DTOs.User;

namespace Infrastructure.RepositoryApi
{
    internal class RepositoryUser : IRepositoryUser
    {
        private readonly ITaskSlayerApiClient _taskSlayerApiClient;
        public RepositoryUser(ITaskSlayerApiClient taskSlayerApiClient) 
        { 
            _taskSlayerApiClient = taskSlayerApiClient;
        }
        public async Task<string> CreateUser(UserDTO user)
        {
            return await _taskSlayerApiClient.CreateUserAsync(user);
        }

        public async Task<UserDTO> GetUser(IUserScopedApiClient userScopedApiClient)
        {
            return await userScopedApiClient.ExecuteAsUserAsync(async action =>
            {
                return await action.GetUserAsync();
            });
        }
    }
}
