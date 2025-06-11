namespace Application.Interfaces
{
    public interface ITokenRefresher
    {
        
        Task<string> RefreshTokenAsync(string userId);

    }
}
