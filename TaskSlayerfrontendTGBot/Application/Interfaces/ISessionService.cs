namespace Application.Interfaces
{
    public interface ISessionService
    {
        public void SetState(long userId, string state);
        public string? GetState(long userId);
        public void ClearState(long userId);
        void SetLanguage(long userId, string langCode);
        string GetLanguage(long userId);
        Task<string> GetTokenAsync(string userId);  
        Task SetTokenAsync(string userId, string token); 
        Task RefreshTokenAsync(string userId);
    }
}
