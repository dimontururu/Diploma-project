using TaskSlayerfrontendTGBot.Interface.IServices;

namespace TaskSlayerfrontendTGBot.Services
{
    public class UserStateService : IUserStateService
    {
        private readonly Dictionary<long, string> _states = new();

        public void SetState(long userId, string state)
        {
            _states[userId] = state;
        }

        public string? GetState(long userId)
        {
            return _states.TryGetValue(userId, out var state) ? state : null;
        }

        public void ClearState(long userId)
        {
            _states.Remove(userId);
        }
    }
}
