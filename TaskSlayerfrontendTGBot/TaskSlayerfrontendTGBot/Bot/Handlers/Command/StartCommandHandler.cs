using Application.Bot;
using Application.Interfaces.Message;
using Application.Services.Domain;
using Telegram.Bot.Types;

namespace Presentation.Bot.Handlers.Command
{
    public class StartCommandHandler : IMessageHandler
    {
        private readonly IUserService _userService;
        private readonly ITelegramMessageService _telegramMessageService;

        public StartCommandHandler(IUserService userService,ITelegramMessageService telegramMessageService) 
        { 
            _userService = userService;
            _telegramMessageService = telegramMessageService;
        }

        public async Task<bool> CanHandle(Update update)
        {
            if (update.Message?.Text == null) return false;

            return update.Message?.Text == "/start";
        }

        public async Task HandleAsync(Update update)
        {
            var message = update.Message;
            var chatId = message.Chat.Id;
            var user = message.From;

            await _telegramMessageService.SendWelcomeMessage(user.Id, chatId);
            await _userService.CreateUser(user);

            Console.WriteLine($"Пользовыатель: {user.Username} (id: {user.Id}) добавлен в базу");
        }
    }
}
