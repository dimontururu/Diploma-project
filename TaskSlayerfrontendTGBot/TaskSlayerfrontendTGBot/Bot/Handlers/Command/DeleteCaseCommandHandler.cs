using Application.Bot;
using Application.Interfaces.Message;
using Application.Services.Domain;
using Application.Session;
using Telegram.Bot.Types;

namespace Presentation.Bot.Handlers.Command
{
    internal class DeleteCaseCommandHandler : IStatefulMessageHandle
    {
        private readonly ISessionService _sessionService;
        private readonly ICaseService _caseService;
        private readonly ITelegramMessageService _telegramMessageService;

        public DeleteCaseCommandHandler(ISessionService sessionService, ICaseService caseService, ITelegramMessageService telegramMessageService)
        {
            _sessionService = sessionService;
            _caseService = caseService;
            _telegramMessageService = telegramMessageService;
        }

        public async Task<bool> CanHandle(Update update)
        {
            return false;
        }

        public async Task<bool> CanHandle(string state, Update update)
        {
            if (update.CallbackQuery == null) return false;

            if (state.StartsWith("InlineButtonDeleteCase/")) return true;

            return false;
        }

        public async  Task HandleAsync(Update update)
        {
            var user = update.CallbackQuery.From;
            var userId = user.Id;
            var message = update.CallbackQuery.Message;
            var chatId = message.Chat.Id;

            try 
            {
                string[] parts = _sessionService.GetState(userId).Split('/');
                string idCase = parts[1];
                await _caseService.DeleteCase(Guid.Parse(idCase), user);
                await _telegramMessageService.SendDeleteCaseEnterMessage(userId, chatId);
            }
            catch 
            { 
            
            }

            _sessionService.ClearState(userId);
        }
    }
}
