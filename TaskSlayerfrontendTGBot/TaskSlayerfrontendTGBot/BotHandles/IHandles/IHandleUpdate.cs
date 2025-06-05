using Telegram.Bot;
using Telegram.Bot.Types;

namespace TaskSlayerfrontendTGBot.BotHandles.IHandles
{
    internal interface IHandleUpdate
    {
        Task HandleUpdateAsync(ITelegramBotClient bot, Update update, CancellationToken cancellationToken);
    }
}
