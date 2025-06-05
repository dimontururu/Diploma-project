using Telegram.Bot;
using Telegram.Bot.Types;

namespace TaskSlayerfrontendTGBot.BotHandles.IHandles
{
    internal interface IHandelCommand
    {
        public Task StartCommand(ITelegramBotClient bot, Update update, CancellationToken cancellationToken);
    }
}
