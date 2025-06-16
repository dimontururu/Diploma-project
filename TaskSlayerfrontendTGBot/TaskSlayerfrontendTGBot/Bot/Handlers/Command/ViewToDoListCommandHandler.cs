using Application.Bot;
using Application.Interfaces.Message;
using Application.Services.Domain;
using Application.Session;
using Telegram.Bot.Types;

namespace Presentation.Bot.Handlers.Command
{
    internal class ViewToDoListCommandHandler : IStatefulMessageHandle
    {
        private readonly ISessionService _sessionService;
        private readonly IToDoListService _toDoListService;
        private readonly ITelegramMessageService _telegramMessageService;
        private readonly ICaseService _caseService;
        
        public ViewToDoListCommandHandler(ISessionService sessionService,IToDoListService toDoListService,ITelegramMessageService telegramMessageService,ICaseService caseService)
        {
            _sessionService = sessionService;
            _telegramMessageService = telegramMessageService;
            _toDoListService = toDoListService;
            _caseService = caseService;
        }
        public async Task<bool> CanHandle(Update update)
        {
            return false;
        }

        public async Task<bool> CanHandle(string state, Update update)
        {
            if (update.CallbackQuery == null) return false;

            if (state.StartsWith("InlineButtonViewToDoList/")) return true;

            return false;
        }

        public async Task HandleAsync(Update update)
        {
            var user = update.CallbackQuery?.From;
            var userId = user.Id;
            var chatId = update.CallbackQuery.Message.Chat.Id;

            string[] parts = _sessionService.GetState(userId).Split('/');
            string idToDoList = parts[1];

            var toDoList = (await _toDoListService.GetToDoLists(user)).FirstOrDefault(x => x.Id == idToDoList);

            if (toDoList != null)
            {
                await _telegramMessageService.SendCasesMesage(user,chatId,toDoList.Name,await _caseService.GetCases(Guid.Parse(toDoList.Id),user), idToDoList);
                Console.WriteLine($"Пользовыатель: {user.Username} (id: {user.Id}) открыл список: {toDoList.Name}");
            }
            _sessionService.ClearState(userId);
        }
    }
}
