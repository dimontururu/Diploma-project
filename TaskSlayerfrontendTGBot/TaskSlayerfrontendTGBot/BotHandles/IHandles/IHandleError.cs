using Telegram.Bot;

namespace TaskSlayerfrontendTGBot.BotHandles.IHandles
{
    internal interface IHandleError
    {
        Task HandleErrorAsync(ITelegramBotClient bot, Exception exception, CancellationToken cancellationToken);
    }
}
