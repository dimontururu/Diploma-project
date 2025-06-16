using Application.Bot;
using Application.Interfaces.Localizer;
using Application.Interfaces.Message;
using Application.Services.Domain;
using Application.Session;
using Infrastructure.Localization;
using Telegram.Bot.Types;

namespace Presentation.Bot.Handlers.Command
{
    internal class ToDoListCommandHandler : IMessageHandler
    {
        private readonly ISessionService _sessionService;
        private readonly ILanguageSelector _languageSelector;
        private readonly IToDoListService _toDoListService;
        private readonly ITelegramMessageService _telegramMessageService;

        public ToDoListCommandHandler(ISessionService sessionService, ILanguageSelector languageSelector,ITelegramMessageService telegramMessageService,IToDoListService toDoListService)
        {
            _sessionService = sessionService;
            _languageSelector = languageSelector;
            _toDoListService = toDoListService;
            _telegramMessageService = telegramMessageService;
        }
        public async  Task<bool> CanHandle(Update update)
        {
            if (update.Message?.Text == null) return false;

            var userId = update.Message.From.Id;

            var localization = new ResxLocalizer(_sessionService.GetLanguage(userId));

            return update.Message?.Text == "/To_do_list" || update.Message?.Text ==  localization["ButtonToDoList"] || _languageSelector.DetectToDoList(update.Message?.Text);
        }

        public async Task HandleAsync(Update update)
        {
            var chatId = update.Message.Chat.Id;
            var user = update.Message.From;
            await _telegramMessageService.SendToDoMessage(user, chatId, await _toDoListService.GetToDoLists(user));

            Console.WriteLine($"Пользовыатель: {user.Username} (id: {user.Id}) вызвал свой список литстов");
        }
    }
}
