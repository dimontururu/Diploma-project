using Application.Interfaces;
using Application.Interfaces.Localizer;
using Application.Interfaces.Message;
using Infrastructure.Localization;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Presentation.Bot.Handlers.Command
{
    internal class LanguageCommandHandler : IStatefulMessageHandle
    {
        private readonly ITelegramBotClient _bot;
        private readonly ISessionService _sessionService;
        private readonly ILanguageSelector _languageSelector;

        public LanguageCommandHandler(ITelegramBotClient bot, ISessionService sessionService, ILanguageSelector languageSelector)
        {
            _bot = bot;
            _sessionService = sessionService;
            _languageSelector = languageSelector;
        }

        public async Task<bool> CanHandle(Update update)
        {
            if (update.Message?.Text == null) return false;

            var chatId = update.Message.Chat.Id;
            var localization = new ResxLocalizer(_sessionService.GetLanguage(chatId));

            var buttonLanguageText = localization["ButtonLanguage"];
            return update.Message?.Text == buttonLanguageText;
        }

        public async Task<bool> CanHandle(string state,Update update)
        {
            if (update.Message?.Text == null) return false;
            return state == "ButtonLanguage";
        }

        public async Task HandleAsync(Update update)
        {
            var message = update.Message;
            if (message == null) return;

            var chatId = message.Chat.Id;
            var localization = new ResxLocalizer(_sessionService.GetLanguage(chatId));

            if (_sessionService.GetState(chatId) == null)
            {
                var replyKeyboard = new ReplyKeyboardMarkup(
                    new[]
                    {
                        new KeyboardButton[]
                        {
                            localization["ButtonLanguageEnglish"],
                            localization["ButtonLanguageRussian"],
                            localization["ButtonLanguageKazakh"]
                        },
                    })
                {
                    ResizeKeyboard = true,
                    OneTimeKeyboard = true
                };

                await _bot.SendMessage(
                    chatId: chatId,
                    text: localization["SelectLanguage"],
                    replyMarkup: replyKeyboard
                );

                _sessionService.SetState(chatId, "ButtonLanguage");
            }
            else
            {
                if (message?.Text == null)
                {
                    await _bot.SendMessage(
                        chatId: chatId,
                        text: localization["SelectLanguage"]
                    );
                    return;
                }
                
                var selectedLanguage = _languageSelector.DetectLanguage(message.Text);

                if (selectedLanguage != null)
                {
                    _sessionService.SetLanguage(chatId, selectedLanguage);
                    localization = new ResxLocalizer(_sessionService.GetLanguage(chatId));

                    var replyKeyboard = new ReplyKeyboardMarkup(new[]
                    {
                        new KeyboardButton[] { localization["ButtonMenu"] }
                    })
                    {
                        ResizeKeyboard = true,
                        OneTimeKeyboard = true
                    };

                    await _bot.SendMessage(
                        chatId: chatId,
                        text: localization["LanguageSetConfirmation"],
                        replyMarkup: replyKeyboard
                    );

                    _sessionService.ClearState(chatId);
                }
                else
                {
                    await _bot.SendMessage(
                        chatId: chatId,
                        text: localization["SelectLanguage"]
                    );
                }
            }
        }
    }
}
