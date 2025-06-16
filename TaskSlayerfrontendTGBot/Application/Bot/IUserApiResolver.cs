using Application.Interfaces.ApiClients;
using Telegram.Bot.Types;

namespace Application.Bot
{
    public interface IUserApiResolver
    {
        IUserScopedApiClient Resolve(User user);
    }
}
