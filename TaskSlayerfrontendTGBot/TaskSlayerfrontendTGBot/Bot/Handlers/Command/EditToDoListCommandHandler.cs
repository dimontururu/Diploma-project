using Application.Bot;
using Application.Interfaces.Message;
using Application.Services.Domain;
using Application.Session;
using Application.Validation;
using Domain.DTOs.ToDoList;
using Telegram.Bot.Types;

namespace Presentation.Bot.Handlers.Command
{
    internal class EditToDoListCommandHandler : IStatefulMessageHandle
    {
        private readonly ISessionService _sessionService;
        private readonly IToDoListService _toDoListService;
        private readonly ITelegramMessageService _telegramMessageService;
        private readonly ITextValidationService _textValidationService;
        private readonly IUserErrorService _userErrorService;

        public EditToDoListCommandHandler(ISessionService sessionService, IToDoListService toDoListService, ITelegramMessageService telegramMessageService, ITextValidationService textValidationService, IUserErrorService userErrorService)
        {
            _sessionService = sessionService;
            _toDoListService = toDoListService;
            _telegramMessageService = telegramMessageService;
            _textValidationService = textValidationService;
            _userErrorService = userErrorService;
        }

        public async Task<bool> CanHandle(Update update)
        {
            return false;
        }

        public async Task<bool> CanHandle(string state, Update update)
        {
            if (update.Message?.Text == null && update.CallbackQuery == null) return false;

            if (state.StartsWith("InlineButtonEditToDoList/")) return true;

            return false;
        }

        public async Task HandleAsync(Update update)
        {
            Message message;
            long chatId;
            User user;
            ReturnToDoListsDTO toDoList;

            if (update.CallbackQuery != null)
            {
                user = update.CallbackQuery?.From;
                var userId = user.Id;
                chatId = update.CallbackQuery.Message.Chat.Id;

                string[] parts = _sessionService.GetState(userId).Split('/');
                string idToDoList = parts[1];

                toDoList = (await _toDoListService.GetToDoLists(user)).FirstOrDefault(x => x.Id == idToDoList);

                if (toDoList != null)
                {
                    await _telegramMessageService.SendEditListEnterMessage(userId, chatId);
                }
                else
                    _sessionService.ClearState(userId);
                return;
            }

            message = update.Message;
            chatId = message.Chat.Id;
            user = message.From;

            if (!_textValidationService.IsValid(message.Text, 50))
            {
                await _userErrorService.SendInvalidNameError(user.Id, chatId);
                return;
            }

            toDoList = (await _toDoListService.GetToDoLists(user)).FirstOrDefault(x => x.Name == message.Text);

            if (toDoList == null)
            {
                string[] parts = _sessionService.GetState(user.Id).Split('/');
                string idToDoList = parts[1];

                var put = new PutToDoListDTO
                {
                    Name = message.Text,
                    Id = Guid.Parse(idToDoList)
                };
                await _toDoListService.EditToDoList(put, user);

                //await _telegramMessageService.SendListAddedSuccessMessage(user.Id, chatId);

                await _telegramMessageService.SendToDoMessage(user, chatId, await _toDoListService.GetToDoLists(user));

                Console.WriteLine($"Пользовыатель: {user.Username} (id: {user.Id}) изменил список: {message.Text}");

                _sessionService.ClearState(user.Id);
            }
            else
            {
                await _telegramMessageService.SendToDoListAlreadyExistsMessage(user.Id, chatId);
                return;
            }
        }
    }
}
