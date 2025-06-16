using Application.Bot;
using Application.Interfaces.Localizer;
using Application.Interfaces.Message;
using Application.Session;
using Infrastructure.Localization;
using Telegram.Bot.Types;

namespace Presentation.Bot.Handlers.Command
{
    internal class MenuCommandHandler :IMessageHandler
    {
        private readonly ISessionService _sessionService;
        private readonly ITelegramMessageService _telegramMessageService;
        private readonly ILanguageSelector _languageSelector;

        public MenuCommandHandler(ISessionService sessionService, ITelegramMessageService telegramMessageService,ILanguageSelector languageSelector)
        {
            _sessionService = sessionService;
            _telegramMessageService = telegramMessageService;
            _languageSelector = languageSelector;
        }

        public async Task<bool> CanHandle(Update update)
        {
            if (update.Message?.Text == null) return false;

            var userId = update.Message.From.Id;

            var localization = new ResxLocalizer(_sessionService.GetLanguage(userId));

            return update.Message?.Text == localization["ButtonMenu"] || _languageSelector.DetectMenu(update.Message?.Text);
        }

        public async Task HandleAsync(Update update)
        {
            var message = update.Message;
            var chatId = message.Chat.Id;
            var user = message.From;
            var userId = user.Id;

            await _telegramMessageService.SendMenuMessage(userId, chatId);

            Console.WriteLine($"Пользовыатель: {user.Username} (id: {user.Id}) вызвал команду menu");
        }
    }
}
