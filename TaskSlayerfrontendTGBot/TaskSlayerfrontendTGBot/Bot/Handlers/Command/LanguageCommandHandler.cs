using Application.Bot;
using Application.Interfaces.Localizer;
using Application.Interfaces.Message;
using Application.Session;
using Infrastructure.Localization;
using Telegram.Bot.Types;

namespace Presentation.Bot.Handlers.Command
{
    internal class LanguageCommandHandler : IStatefulMessageHandle
    {
        private readonly ISessionService _sessionService;
        private readonly ILanguageSelector _languageSelector;
        private readonly ITelegramMessageService _telegramMessageService;

        public LanguageCommandHandler(ISessionService sessionService, ILanguageSelector languageSelector,ITelegramMessageService telegramMessageService)
        {
            _sessionService = sessionService;
            _languageSelector = languageSelector;
            _telegramMessageService = telegramMessageService;
        }

        public async Task<bool> CanHandle(Update update)
        {
            if (update.Message?.Text == null) return false;

            var userId = update.Message.From.Id;
            var localization = new ResxLocalizer(_sessionService.GetLanguage(userId));

            var buttonLanguageText = localization["ButtonLanguage"];
            return update.Message?.Text == buttonLanguageText || _languageSelector.DetectLanguageCommand(update.Message?.Text);
        }

        public async Task<bool> CanHandle(string state,Update update)
        {
            if (update.Message?.Text == null) return false;
            return state == "ButtonLanguage";
        }

        public async Task HandleAsync(Update update)
        {
            var message = update.Message;
            var chatId = message.Chat.Id;
            var user = message.From;
            var userId = user.Id;

            if (_sessionService.GetState(chatId) == null)
            {
                await _telegramMessageService.SendSelectLanguageKeyboardMessage(userId, chatId);

                _sessionService.SetState(chatId, "ButtonLanguage");
            }
            else
            {
                if (message?.Text == null)
                {
                    await _telegramMessageService.SendSelectLanguageMessage(userId, chatId);
                    return;
                }
                
                var selectedLanguage = _languageSelector.DetectLanguage(message.Text);

                if (selectedLanguage != null)
                {
                    _sessionService.SetLanguage(userId, selectedLanguage);

                    await _telegramMessageService.SendLanguageSetConfirmationMessage(userId, chatId);

                    _sessionService.ClearState(userId);

                    Console.WriteLine($"Пользовыатель: {user.Username} (id: {user.Id}) сменил язык");
                }
                else
                    await _telegramMessageService.SendSelectLanguageMessage(userId,chatId);
            }
        }
    }
}
