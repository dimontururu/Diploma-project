using Telegram.Bot.Types;

namespace Application.Bot
{
    public interface IBotUpdateHandler
    {
        Task HandleAsync(Update update);
    }
}
