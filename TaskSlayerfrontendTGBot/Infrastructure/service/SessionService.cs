using Application.Interfaces;

namespace Infrastructure.service
{
    internal class SessionService : ISessionService
    {
        private readonly Dictionary<long, string> _userStates = new();
        private readonly Dictionary<long, string> _userLangs = new();
        private readonly IUserTokenStorage _userTokenStorage;

        public SessionService(IUserTokenStorage userTokenStorage)
        {
            _userTokenStorage = userTokenStorage;
        }

        public void SetState(long userId, string state) => _userStates[userId] = state;
        public string? GetState(long userId) => _userStates.TryGetValue(userId, out var s) ? s : null;
        public void ClearState(long userId) => _userStates.Remove(userId);
        public void SetLanguage(long userId, string langCode) => _userLangs[userId] = langCode;
        public string GetLanguage(long userId) => _userLangs.TryGetValue(userId, out var lang) ? lang : "ru";

        public async Task<string> GetTokenAsync(string userId)
        {
            var token = await _userTokenStorage.GetTokenAsync(userId);
            if (token == null)
            {
                throw new Exception("Token not found");
            }
            return token;
        }

        public async Task SetTokenAsync(string userId, string token)=>await _userTokenStorage.SetTokenAsync(userId, token);

        public async Task RefreshTokenAsync(string userId,string token)=>await SetTokenAsync(userId, token);
    }
}
