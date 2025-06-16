using Application.Interfaces.ApiClients;
using Application.Session;
using Domain.DTOs.User;
using Infrastructure.Api;

namespace Infrastructure.service
{
    internal class SessionService : ISessionService
    {
        private readonly Dictionary<long, string> _userStates = new();
        private readonly Dictionary<long, string> _userLangs = new();
        private readonly Dictionary<UserDTO, IUserScopedApiClient> _userApi = new();

        private readonly HttpClient _httpClient;
        private readonly ITaskSlayerApiClient _taskSlayerApiClient;

        public SessionService(HttpClient http, ITaskSlayerApiClient taskSlayerApiClient)
        {
            _httpClient = http;
            _taskSlayerApiClient = taskSlayerApiClient;
        }

        public void SetState(long userId, string state) => _userStates[userId] = state;
        public string? GetState(long userId) => _userStates.TryGetValue(userId, out var s) ? s : null;
        public void ClearState(long userId) => _userStates.Remove(userId);
        public void SetLanguage(long userId, string langCode) => _userLangs[userId] = langCode;
        public string GetLanguage(long userId) => _userLangs.TryGetValue(userId, out var lang) ? lang : "ru";
        public IUserScopedApiClient GetApi(UserDTO user)
        {
            var result = _userApi.GetValueOrDefault(user);
            if (result == null)
            {
                result =  new UserScopedApiClient(_httpClient, Environment.GetEnvironmentVariable("base__Url"),user,_taskSlayerApiClient);
                _userApi[user] = result;
            }

            return result;
        }
    }
}
