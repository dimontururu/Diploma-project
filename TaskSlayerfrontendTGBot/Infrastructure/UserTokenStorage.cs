using Application.Interfaces;
using System.Collections.Concurrent;

namespace Infrastructure
{
    public class UserTokenStorage : IUserTokenStorage
    {
        private readonly ConcurrentDictionary<string, string> _tokens = new();

        public Task<string?> GetTokenAsync(string userId)
        {
            _tokens.TryGetValue(userId, out var token);
            return Task.FromResult(token);
        }

        public Task SetTokenAsync(string userId, string token)
        {
            _tokens[userId] = token;
            return Task.CompletedTask;
        }

        public Task RemoveTokenAsync(string userId)
        {
            _tokens.TryRemove(userId, out _);
            return Task.CompletedTask;
        }
    }
}
