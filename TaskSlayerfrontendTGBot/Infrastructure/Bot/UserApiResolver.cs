using Application.Bot;
using Application.Interfaces.ApiClients;
using Application.Session;
using Domain.DTOs.User;
using Telegram.Bot.Types;

namespace Infrastructure.Bot
{
    internal class UserApiResolver : IUserApiResolver
    {
        private readonly ISessionService _sessionService;

        public UserApiResolver(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        public IUserScopedApiClient Resolve(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var userDTO = new UserDTO
            {
                Id = user.Id.ToString(),
                Name = user.Username,
                Type_id = "Telegram"
            };

            return _sessionService.GetApi(userDTO);
        }
    }
}
