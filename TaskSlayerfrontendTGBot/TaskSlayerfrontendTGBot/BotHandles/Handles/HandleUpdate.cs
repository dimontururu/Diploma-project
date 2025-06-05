using TaskSlayerfrontendTGBot.BotHandles.IHandles;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TaskSlayerfrontendTGBot.BotHandles.Handles
{
    internal class HandleUpdate:IHandleUpdate
    {
        private readonly IHandelCommand _handelCommand;
        public HandleUpdate(IHandelCommand handelCommand) 
        {
            _handelCommand = handelCommand;
        }
        public async Task HandleUpdateAsync(ITelegramBotClient bot, Update update, CancellationToken cancellationToken)
        {
            var message = update.Message;
            var chatId = message.Chat.Id;
            var user = message.From;

            Console.WriteLine($"[{user?.Id}] {user?.FirstName}: {message.Text ?? message.Type.ToString()}");

            if (message.Text == "/start")
            {
                await _handelCommand.StartCommand(bot, update, cancellationToken);
                return;
            }

            string welcomeText = $"/help — Не бойся просить помощи! 💙";

            await bot.SendMessage(
                chatId: chatId,
                text: welcomeText,
                cancellationToken: cancellationToken
            );
            return;
        }
    }
}
