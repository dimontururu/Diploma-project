using Application.Bot;
using Application.Interfaces.Localizer;
using Application.Interfaces.Message;
using Application.Services.Domain;
using Application.Session;
using Application.Validation;
using Infrastructure.Localization;
using Telegram.Bot.Types;

namespace Presentation.Bot.Handlers.Command
{
    internal class DeleteToDoListCommandHandler : IStatefulMessageHandle
    {
        private readonly ISessionService _sessionService;
        private readonly ILanguageSelector _languageSelector;
        private readonly ITextValidationService _textValidationService;
        private readonly IUserErrorService _userErrorService;
        private readonly ITelegramMessageService _telegramMessageService;
        private readonly IToDoListService _toDoListService;
        public DeleteToDoListCommandHandler(ILanguageSelector language, ISessionService sessionService, ITextValidationService textValidationService, IUserErrorService userErrorService, IToDoListService toDoListService, ITelegramMessageService telegramMessageService)
        {
            _languageSelector = language;
            _sessionService = sessionService;
            _textValidationService = textValidationService;
            _userErrorService = userErrorService;
            _toDoListService = toDoListService;
            _telegramMessageService = telegramMessageService;
        }
        public async Task<bool> CanHandle(Update update)
        {

            if (update.Message?.Text == null) return false;

            var userId = update.Message.From.Id;

            var localization = new ResxLocalizer(_sessionService.GetLanguage(userId));

            return update.Message?.Text == localization["ButtonDeleteToDoList"] || _languageSelector.DetectDeleteToDoList(update.Message.Text);
        }

        public async Task<bool> CanHandle(string state, Update update)
        {

            if (update.Message?.Text == null && update.CallbackQuery == null) return false;

            if (state == "ButtonDeleteToDoList" || state.StartsWith("InlineButtonDeleteToDoList/")) return true;

            return false;
        }

        public async Task HandleAsync(Update update)
        {
            Message message;
            long chatId;
            User user;

            if (update.CallbackQuery != null)
            {
                user = update.CallbackQuery?.From;
                var userId = user.Id;
                chatId = update.CallbackQuery.Message.Chat.Id;

                string[] parts = _sessionService.GetState(userId).Split('/');
                string idToDoList = parts[1];

                var toDoList = (await _toDoListService.GetToDoLists(user)).FirstOrDefault(x => x.Id == idToDoList);

                if (toDoList != null)
                {
                    await _toDoListService.DeleteToDoList(Guid.Parse(idToDoList), user);

                    await _telegramMessageService.SendListDeleteedSuccessMessage(user.Id, chatId);

                    await _telegramMessageService.SendToDoMessage(user, chatId, await _toDoListService.GetToDoLists(user));

                    Console.WriteLine($"Пользовыатель: {user.Username} (id: {user.Id}) удалил список: а какой хуй знает");
                }
                _sessionService.ClearState(user.Id);

                return;
            }

            message = update.Message;
            chatId = message.Chat.Id;
            user = message.From;

            if (_sessionService.GetState(user.Id) == null)
            {
                if ((await _toDoListService.GetToDoLists(user)).Count == 0)
                    return;

                await _telegramMessageService.SendDeleteListEnterMessage(user.Id, chatId);

                _sessionService.SetState(user.Id, "ButtonDeleteToDoList");
            }
            else
            {
                if (_textValidationService.ContainsInvalidCharacters(message.Text))
                {
                    await _userErrorService.SendInvalidNameError(user.Id, chatId);
                    return;
                }
                
                var toDoList = (await _toDoListService.GetToDoLists(user)).FirstOrDefault(x => x.Name == message.Text);

                if (toDoList != null)
                {
                    await _toDoListService.DeleteToDoList(Guid.Parse(toDoList.Id), user);

                    await _telegramMessageService.SendListDeleteedSuccessMessage(user.Id, chatId);

                    await _telegramMessageService.SendToDoMessage(user, chatId, await _toDoListService.GetToDoLists(user));

                    Console.WriteLine($"Пользовыатель: {user.Username} (id: {user.Id}) удалил список: {message.Text}");

                    _sessionService.ClearState(user.Id);
                }
                else 
                {
                    await _telegramMessageService.SendDeleteListEnterMessage(user.Id, chatId);
                    return;
                }
            }
        }
    }
}
