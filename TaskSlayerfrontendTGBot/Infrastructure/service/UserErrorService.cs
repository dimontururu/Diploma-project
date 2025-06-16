using Application.Bot;
using Application.Services.Domain;
using Application.Session;
using Infrastructure.Localization;
using Telegram.Bot;

namespace Infrastructure.service
{
    internal class UserErrorService : IUserErrorService
    {
        private readonly ITelegramBotClient _bot;
        private readonly ISessionService _sessionService;
        private readonly ITelegramKeyboardService _telegramKeyboardService;
        public UserErrorService(ITelegramBotClient bot,ISessionService sessionService, ITelegramKeyboardService telegramKeyboardService) 
        { 
            _bot = bot;
            _sessionService = sessionService;
            _telegramKeyboardService = telegramKeyboardService;
        }
        public async Task SendInvalidNameError(long userId,long chatId)
        {
            var localization = new ResxLocalizer(_sessionService.GetLanguage(userId));
            await _bot.SendMessage(chatId, localization["ErrorInvalidNameOrTooLong"]);
        }

        public async Task ListLimitError(long userId,long chatId)
        {
            var localization = new ResxLocalizer(_sessionService.GetLanguage(userId));
            await _bot.SendMessage(chatId, localization["ListLimitError"],replyMarkup: _telegramKeyboardService.GetMainMenuKeyboard(chatId));
        }

        public async Task SendDateError(long userId,long chatId)
        {
            var localization = new ResxLocalizer(_sessionService.GetLanguage(userId));
            await _bot.SendMessage(chatId, localization["ErrorInvalidDate"]);
        }
    }
}
