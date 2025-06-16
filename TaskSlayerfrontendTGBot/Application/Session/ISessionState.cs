namespace Application.Session
{
    public interface ISessionState
    {
        public void SetState(long userId, string state);
        public string? GetState(long userId);
        public void ClearState(long userId);
    }
}
