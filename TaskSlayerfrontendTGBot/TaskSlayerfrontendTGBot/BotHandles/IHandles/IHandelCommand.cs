using Telegram.Bot;
using Telegram.Bot.Types;

namespace TaskSlayerfrontendTGBot.BotHandles.IHandles
{
    internal interface IHandelCommand
    {
        Task StartCommand(ITelegramBotClient bot, Update update, CancellationToken cancellationToken);
        Task To_do_listCommand(ITelegramBotClient bot, Update update, CancellationToken cancellationToken);
    }
}
