using Telegram.Bot;

namespace Application.Interfaces
{
    public interface IHandleError
    {
        Task HandleErrorAsync(ITelegramBotClient bot, Exception exception, CancellationToken cancellationToken);
    }
}
