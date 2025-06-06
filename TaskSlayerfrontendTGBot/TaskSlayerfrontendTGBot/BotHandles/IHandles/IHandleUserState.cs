using Telegram.Bot;
using Telegram.Bot.Types;

namespace TaskSlayerfrontendTGBot.BotHandles.IHandles
{
    internal interface IHandleUserState
    {
        Task HandleUserStateAsync(ITelegramBotClient bot, Update update, CancellationToken cancellationToken);
    }
}
