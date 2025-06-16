using Application.Bot;
using Application.Interfaces.Localizer;
using Application.Interfaces.Message;
using Application.Services.Domain;
using Application.Session;
using Application.Validation;
using Domain.DTOs.Case;
using System.Globalization;
using Telegram.Bot.Types;

namespace Presentation.Bot.Handlers.Command
{
    internal class ADDCaseCommandHandler: IStatefulMessageHandle
    {
        private readonly ISessionService _sessionService;
        private readonly ITextValidationService _textValidationService;
        private readonly IUserErrorService _userErrorService;
        private readonly ITelegramMessageService _telegramMessageService;
        private readonly IToDoListService _toDoListService;
        private readonly ICaseService _caseService;


        public ADDCaseCommandHandler(ISessionService sessionService, ITextValidationService textValidationService, IUserErrorService userErrorService, IToDoListService toDoListService, ITelegramMessageService telegramMessageService, ICaseService caseService)
        {
            _sessionService = sessionService;
            _textValidationService = textValidationService;
            _userErrorService = userErrorService;
            _toDoListService = toDoListService;
            _telegramMessageService = telegramMessageService;
            _caseService = caseService;
        }
        public async Task<bool> CanHandle(Update update)
        {
            return false;
        }

        public async Task<bool> CanHandle(string state, Update update)
        {
            if (update.Message?.Text == null && update.CallbackQuery == null) return false;

            if (state.StartsWith("InlineButtonADDCase/")) return true;

            return false;
        }

        public async Task HandleAsync(Update update)
        {
            var user = update.Message?.From ?? update.CallbackQuery?.From;
            var userId = user.Id;
            var message = update.CallbackQuery?.Message ?? update.Message;
            var chatId = message.Chat.Id;
            

            string[] parts = _sessionService.GetState(userId).Split('/');
            string idToDoList = parts[1];
            string action =  parts[0];
            string name = parts.Length > 2 ? parts[2] : null;

            var toDoList = (await _toDoListService.GetToDoLists(user)).FirstOrDefault(x => x.Id == idToDoList);

            if (toDoList != null)
            {
                if (name == null)
                {
                    await _telegramMessageService.SendAddCaseEnterMessage(user.Id, chatId);
                    var status = _sessionService.GetState(user.Id);
                    _sessionService.ClearState(userId);
                    status += "/Name";
                    _sessionService.SetState(user.Id, status);
                    return;
                }

                if (name == "Name")
                {
                    if (!_textValidationService.IsValid(message.Text, 50) || message.Text == "Name")
                    {
                        await _userErrorService.SendInvalidNameError(user.Id, chatId);
                        return;
                    }

                    await _telegramMessageService.SendDateCaseEnter(user.Id, chatId);
                    var status = $"{action}/{idToDoList}/{update.Message.Text}";
                    _sessionService.ClearState(userId);
                    _sessionService.SetState(user.Id, status);
                    return;
                }
                else
                {
                    if(!DateTimeOffset.TryParseExact(message.Text,"dd.MM.yyyy",CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTimeOffset result))
                    {
                        await _userErrorService.SendDateError(user.Id, chatId);
                        return;
                    }
                    var caseDTO = new NewCaseDTO()
                    {
                        Name = name,
                        Id_to_do_list = Guid.Parse(idToDoList),
                        DateEnd = result,
                        DateOfCreation = DateTimeOffset.UtcNow
                    };
                    await _caseService.CreateCase(Guid.Parse(idToDoList),caseDTO,user);
                    await _telegramMessageService.SendTrueCreateCase(user.Id, chatId);
                }
                    await _telegramMessageService.SendCasesMesage(user, chatId, toDoList.Name, await _caseService.GetCases(Guid.Parse(toDoList.Id), user),idToDoList);
                Console.WriteLine($"Пользовыатель: {user.Username} (id: {user.Id}) Добавил задачу в список: {toDoList.Name}");
            }
            _sessionService.ClearState(userId);
        }
    }
}
