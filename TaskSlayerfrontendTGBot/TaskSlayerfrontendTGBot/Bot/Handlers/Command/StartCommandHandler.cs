using Application.Interfaces;
using Application.Interfaces.Message;
using Infrastructure.Localization;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Presentation.Bot.Handlers.Command
{
    public class StartCommandHandler : IMessageHandler
    {
        private readonly ITelegramBotClient _bot;
        private readonly ISessionService _sessionService;
        private readonly IApiRetryHelper _apiRetryHelper;

        public StartCommandHandler(ITelegramBotClient bot, ISessionService sessionService, IApiRetryHelper apiRetryHelper) 
        { 
            _bot = bot;
            _sessionService = sessionService;
            _apiRetryHelper = apiRetryHelper;
        }

        public async Task<bool> CanHandle(Update update)
        {
            if (update.Message?.Text == null) return false;

            var chatId = update.Message?.Chat?.Id;

            var localization = new ResxLocalizer(_sessionService.GetLanguage(chatId.Value));

            return update.Message?.Text == "/start" || update.Message?.Text == localization["ButtonMenu"];
        }

        public async Task HandleAsync(Update update)
        {
            var message = update.Message;
            var chatId = message.Chat.Id;
            var user = message.From;

            var localization = new ResxLocalizer(_sessionService.GetLanguage(chatId));

            string welcomeText = localization["Welcome"];

            var replyKeyboard = new ReplyKeyboardMarkup
                (new[]{
                    new KeyboardButton[] {localization["ButtonSetting"]},
                })
            {
                ResizeKeyboard = true,
                OneTimeKeyboard = true
            };

            await _bot.SendMessage(
                chatId: chatId,
                text: welcomeText,
                replyMarkup: replyKeyboard
            );
            
            _apiRetryHelper.ExecuteWithReauthAsync(chatId, async _ =>
            {
                return await _apiClient.GetToDoListsAsync(userId);
            });
        }
    }
}
