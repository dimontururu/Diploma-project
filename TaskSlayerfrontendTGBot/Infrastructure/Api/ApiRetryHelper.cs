using Application.Interfaces;

namespace Infrastructure.Api
{
    public class ApiRetryHelper:IApiRetryHelper
    {
        private readonly ISessionService _sessionService;
        private readonly ITokenRefresher _tokenRefresher;

        public ApiRetryHelper(
            ISessionService sessionService,
            ITokenRefresher tokenRefresher)
        {
            _sessionService = sessionService;
            _tokenRefresher = tokenRefresher;
        }

        public async Task<T> ExecuteWithReauthAsync<T>(string userId, Func<string, Task<T>> apiCall)
        {
            var token = await _sessionService.GetTokenAsync(userId);
            try
            {
                return await apiCall(token);
            }
            catch (HttpRequestException ex) when (ex.Message.Contains("401"))
            {
                var newToken = await tokenRefresher(userId);
                await _sessionService.SetTokenAsync(userId, newToken);

                return await apiCall(newToken);
            }
        }
    }
}
