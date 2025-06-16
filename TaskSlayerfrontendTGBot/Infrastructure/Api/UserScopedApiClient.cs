using Application.Interfaces.ApiClients;
using Domain.DTOs.User;
using System.Net.Http.Headers;

namespace Infrastructure.Api
{
    internal class UserScopedApiClient: IUserScopedApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly UserDTO _user;
        private string _token;
        private readonly ITaskSlayerApiClient _client;
        public UserScopedApiClient(HttpClient httpClient,string baseUrl, UserDTO user, ITaskSlayerApiClient client) 
        {
            _httpClient = httpClient;
            _user = user;
            _client = client;
            try
            {
                _token = client.AuthorizationAsync(user).Result;
            }
            catch
            {
                _token = null;
            }
        }

        public async Task<T> ExecuteAsUserAsync<T>(Func<ITaskSlayerApiClient, Task<T>> action)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
                Task<T> result;
                try
                {
                    result = action(_client);
                    return await result;
                }
                catch (ApiException ex)/* when (ex.StatusCode == (int)HttpStatusCode.Unauthorized)*/
                {
                    _token = _client.AuthorizationAsync(_user).Result;
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
                    result = action(_client);
                    return await result;
                }
                
            }
            finally
            {
                _httpClient.DefaultRequestHeaders.Authorization = null;
            }
        }

        public async Task ExecuteAsUserAsync(Func<ITaskSlayerApiClient, Task> action)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

                try
                {
                    await action(_client);
                }
                catch (ApiException ex)
                {
                    _token = _client.AuthorizationAsync(_user).Result;
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
                    await action(_client);
                }
            }
            finally
            {
                _httpClient.DefaultRequestHeaders.Authorization = null;
            }
        }
    }
}
