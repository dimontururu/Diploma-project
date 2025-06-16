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
    internal class ADDToDoListCommandHandler : IStatefulMessageHandle
    {
        private readonly ISessionService _sessionService;
        private readonly ILanguageSelector _languageSelector;
        private readonly ITextValidationService _textValidationService;
        private readonly IUserErrorService _userErrorService;
        private readonly ITelegramMessageService _telegramMessageService;
        private readonly IToDoListService _toDoListService;


        public ADDToDoListCommandHandler(ILanguageSelector language,ISessionService sessionService,ITextValidationService textValidationService,IUserErrorService userErrorService,IToDoListService toDoListService,ITelegramMessageService telegramMessageService)
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

            return update.Message?.Text == localization["ButtonADDToDoList"] || _languageSelector.DetectADDToDoList(update.Message.Text);
        }

        public async Task<bool> CanHandle(string state, Update update)
        {
            if (update.Message?.Text == null) return false;
            if (state == "ButtonADDToDoList") return true;
            return false;
        }

        public async Task HandleAsync(Update update)
        {
            var message = update.Message;
            var chatId = message.Chat.Id;
            var user = message.From;

            if (_sessionService.GetState(user.Id) == null)
            {
                var toDoLists = await _toDoListService.GetToDoLists(user);

                if (toDoLists.Count >= 10)
                {
                    await _userErrorService.ListLimitError(user.Id,chatId);
                    return;
                }

                await _telegramMessageService.SendAddListEnterMessage(user.Id, chatId);

                _sessionService.SetState(user.Id, "ButtonADDToDoList");
            }
            else
            {
                if (!_textValidationService.IsValid(message.Text, 50))
                {
                    await _userErrorService.SendInvalidNameError(user.Id, chatId);
                    return;
                }

                var toDoList = (await _toDoListService.GetToDoLists(user)).FirstOrDefault(x => x.Name == message.Text);

                if (toDoList == null)
                {
                    await _toDoListService.CreateToDoList(message.Text, user);

                    await _telegramMessageService.SendListAddedSuccessMessage(user.Id, chatId);

                    await _telegramMessageService.SendToDoMessage(user, chatId, await _toDoListService.GetToDoLists(user));

                    Console.WriteLine($"Пользовыатель: {user.Username} (id: {user.Id}) добавил новый список: {message.Text}");

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
}
