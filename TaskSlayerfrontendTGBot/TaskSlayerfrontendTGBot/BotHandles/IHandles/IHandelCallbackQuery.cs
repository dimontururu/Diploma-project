using Telegram.Bot;
using Telegram.Bot.Types;

namespace TaskSlayerfrontendTGBot.BotHandles.IHandles
{
    internal interface IHandelCallbackQuery
    {
        Task HandelCallbackQueryAsync(ITelegramBotClient bot, Update update, CancellationToken cancellationToken);
    }
}
