using Application.Bot;
using Application.Interfaces.Localizer;
using Application.Interfaces.Message;
using Application.Session;
using Infrastructure.Localization;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Presentation.Bot.Handlers.Command
{
    internal class SettingCommandHandler : IMessageHandler
    {
        private readonly ITelegramMessageService _messageService;
        private readonly ISessionService _sessionService;
        private readonly ILanguageSelector _languageSelector;

        public SettingCommandHandler(ITelegramBotClient bot, ISessionService sessionService, ILanguageSelector languageSelector,ITelegramMessageService messageService)
        {
            _messageService = messageService;
            _sessionService = sessionService;
            _languageSelector = languageSelector;
        }

        public async Task<bool> CanHandle(Update update)
        {
            if (update.Message?.Text == null) return false;

            var userId = update.Message.From.Id;
            var localization = new ResxLocalizer(_sessionService.GetLanguage(userId));

            return update.Message?.Text == localization["ButtonSetting"] || _languageSelector.DetectSetting(update.Message?.Text);
        }

        public async Task HandleAsync(Update update)
        {
            var message = update.Message;
            var chatId = message.Chat.Id;
            var user = message.From;
            var userId = user.Id;

            await _messageService.SendWhatWouldYouChangeMessage(userId, chatId);

            Console.WriteLine($"Пользовыатель: {user.Username} (id: {user.Id}) вызвал команду setting");
        }
    }
}
