namespace Application.Interfaces
{
    public interface IApiRetryHelper
    {
        Task<T> ExecuteWithReauthAsync<T>(string userId, Func<string, Task<T>> apiCall);
    }
}
