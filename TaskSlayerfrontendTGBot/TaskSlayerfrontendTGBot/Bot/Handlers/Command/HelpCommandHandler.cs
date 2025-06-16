using Application.Bot;
using Application.Interfaces.Message;
using Telegram.Bot.Types;

namespace Presentation.Bot.Handlers.Command
{
    internal class HelpCommandHandler : IMessageHandler
    {
        private readonly ITelegramMessageService _telegramMessageService;

        public HelpCommandHandler(ITelegramMessageService telegramMessageService)
        {
            _telegramMessageService = telegramMessageService;
        }

        public async Task<bool> CanHandle(Update update)
        {
            if (update.Message?.Text == null) return false;

            return update.Message?.Text == "/help";
        }

        public async Task HandleAsync(Update update)
        {
            var message = update.Message;
            var chatId = message.Chat.Id;
            var user = message.From;
            var userId = user.Id;

            await _telegramMessageService.SendHelpMessage(userId, chatId);

            Console.WriteLine($"Пользовыатель: {user.Username} (id: {user.Id}) воспользовался командой /help");
        }
    }
}
