using Telegram.Bot.Types;

namespace Application.Services.Domain
{
    public interface IUserService
    {
        Task CreateUser(User userTelegram);
    }
}
