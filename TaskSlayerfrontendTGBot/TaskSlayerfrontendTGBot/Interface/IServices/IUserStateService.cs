namespace TaskSlayerfrontendTGBot.Interface.IServices
{
    public interface IUserStateService
    {
        void SetState(long userId, string state);
        string? GetState(long userId);
        void ClearState(long userId);
    }
}
