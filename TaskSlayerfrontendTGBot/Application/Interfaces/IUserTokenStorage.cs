namespace Application.Interfaces
{
    public interface IUserTokenStorage
    {
        Task<string?> GetTokenAsync(string userId);
        Task SetTokenAsync(string userId, string token);
        Task RemoveTokenAsync(string userId);
    }
}
