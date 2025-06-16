using Application.Bot;
using Application.Interfaces.Message;
using Application.Services.Domain;
using Application.Session;
using Domain.DTOs.Award;
using Domain.DTOs.Case;
using Domain.DTOs.ToDoList;
using System.Collections.Generic;
using Telegram.Bot.Types;

namespace Presentation.Bot.Handlers.Command
{
    internal class EditCaseCommandHandler : IStatefulMessageHandle
    {
        private readonly ISessionService _sessionService;
        private readonly IAwardService _awardService;
        private readonly ICaseService _caseService;
        private readonly ITelegramMessageService _telegramMessageService;
        private readonly IToDoListService _toDoListService;

        public EditCaseCommandHandler(ISessionService sessionService, IAwardService awardService, ICaseService caseService, ITelegramMessageService telegramMessageService, IToDoListService toDoListService)
        {
            _sessionService = sessionService;
            _awardService = awardService;
            _caseService = caseService;
            _telegramMessageService = telegramMessageService;
            _toDoListService = toDoListService;
        }

        public async Task<bool> CanHandle(string state, Update update)
        {
            if (update.CallbackQuery == null) return false;

            if (state.StartsWith("InlineButtonEditCase/")) return true;

            return false;
        }

        public async Task<bool> CanHandle(Update update)
        {
            return false;
        }

        public async Task HandleAsync(Update update)
        {
            var user = update.Message?.From ?? update.CallbackQuery?.From;
            var userId = user.Id;
            var message = update.CallbackQuery?.Message ?? update.Message;
            var chatId = message.Chat.Id;


            string[] parts = _sessionService.GetState(userId).Split('/');
            string idCase = parts[1];

            try 
            {
                var toDoLists = await _toDoListService.GetToDoLists(user);

                ReturnCaseDTO foundCase = null;
                ReturnToDoListsDTO toDoList = null;

                foreach (var list in toDoLists)
                {
                    var cases = await _caseService.GetCases(Guid.Parse(list.Id), user);

                    foundCase = cases.FirstOrDefault(c => c.Id == Guid.Parse(idCase));

                    if (foundCase != null)
                    {
                        toDoList = list;
                        break;
                    }
                }

                if (foundCase.Status)
                    throw new Exception();
                var putCase = new PutCaseDTO
                {
                    Id = Guid.Parse(idCase),
                    Name = foundCase.Name,
                    Status = true,
                    DateEnd = foundCase.DateEnd
                };
                await _caseService.PutCase(putCase, user);

                var random = new Random();
                bool randomBool = random.Next(100) < 20;

                if (randomBool)
                {
                    ICollection<ReturnAwardDTO> award = await _awardService.GetAwards(user);
                    ICollection<ReturnAwardDTO> userAward = await _awardService.GetUserAwarads(user);

                    var userAwardIds = userAward.Select(a => a.Id).ToHashSet();

                    var availableAwards = award.Where(a => !userAwardIds.Contains(a.Id)).ToList();

                    if (availableAwards.Any())
                    {
                        var randomIndex = random.Next(availableAwards.Count);
                        var selectedAward = availableAwards[randomIndex];

                        await _awardService.SetUserAwarad(selectedAward.Id, user);
                        await _telegramMessageService.SendTrueGetAwardCase(user.Id, chatId);
                    }
                }
                await _telegramMessageService.SendCasesMesage(user, chatId, toDoList.Name, await _caseService.GetCases(Guid.Parse(toDoList.Id), user), toDoList.Id);
            }
            catch 
            { }

            _sessionService.ClearState(userId);
        }
    }
}
