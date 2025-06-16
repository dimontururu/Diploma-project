using Application.Interfaces.Message;
using Application.Session;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Presentation.Bot.Handlers.Command
{
    internal class InlineKeyboardButtonCommandHandler : IMessageHandler
    {
        private readonly ISessionService _sessionService;
        private readonly ITelegramBotClient _bot;

        public InlineKeyboardButtonCommandHandler(ISessionService sessionService,ITelegramBotClient bot)
        {
            _sessionService = sessionService;
            _bot = bot;
        }
        public async Task<bool> CanHandle(Update update)
        {
            if (update.CallbackQuery != null)
                HandleAsync(update);
            return false;
        }

        public async Task HandleAsync(Update update)
        {
            var userId = update.CallbackQuery?.From?.Id;
            _sessionService.SetState(userId.Value,update.CallbackQuery.Data);
            await _bot.AnswerCallbackQuery(update.CallbackQuery.Id);
        }
    }
}
