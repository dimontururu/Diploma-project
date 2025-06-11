using Application.Interfaces;
using Infrastructure.Api;

namespace Infrastructure
{
    public class TokenRefresher : ITokenRefresher
    {
        private readonly swaggerClient _apiClient;

        public TokenRefresher(swaggerClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<string> RefreshTokenAsync(string userId)
        {
            var user = new UserDTO
            {
                Name="Пусто",
                Id=userId,
                Type_id = "Telegram"
            };
            return await _apiClient.AuthorizationAsync(user);
        }
    }
}
