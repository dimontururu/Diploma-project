namespace Application.Session
{
    public interface ISessionLenguage
    {
        void SetLanguage(long userId, string langCode);
        string GetLanguage(long userId);
    }
}
