using Application.Bot;
using Application.Services.Domain;
using Application.Session;
using Domain.DTOs.Award;
using Domain.DTOs.Case;
using Domain.DTOs.ToDoList;
using Infrastructure.Localization;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Infrastructure.service.Bot
{
    internal class TelegramMessageService : ITelegramMessageService
    {
        private readonly ISessionService _sessionService;
        private readonly ITelegramBotClient _bot;
        private readonly ITelegramKeyboardService _telegramKeyboardService;
        private readonly ICaseService _caseService;
        public TelegramMessageService(ISessionService sessionService,ITelegramBotClient bot, ITelegramKeyboardService telegramKeyboardService,ICaseService caseService) 
        {
            _bot = bot;
            _sessionService = sessionService;
            _telegramKeyboardService = telegramKeyboardService;
            _caseService = caseService;
        }
        public async Task SendWelcomeMessage(long userId,long chatId)
        {
            var localization = new ResxLocalizer(_sessionService.GetLanguage(userId));

            string welcomeText = localization["Welcome"];

            await _bot.SendMessage(
                chatId: chatId,
                text: welcomeText,
                replyMarkup: _telegramKeyboardService.GetMainMenuKeyboard(chatId)
            );
        }

        public async Task SendToDoMessage(User user, long chatId,ICollection<ReturnToDoListsDTO> toDoLists)
        {
            var userId = user.Id;

            var localization = new ResxLocalizer(_sessionService.GetLanguage(userId));

            if (toDoLists.Count == 0)
            {
                await _bot.SendMessage(
                    chatId: chatId,
                    text: localization["RequestCreateToDoList"],
                    replyMarkup: _telegramKeyboardService.GetAddToDoAndMenuKeyboard(userId)
                );
            }
            else
            {
                string text =
                        $"{localization["YourLists"]}\n" +
                        $"\n";

                await _bot.SendMessage(
                    chatId: chatId,
                    text: text,
                    replyMarkup: _telegramKeyboardService.GetToDoActionsKeyboard(userId),
                    parseMode: Telegram.Bot.Types.Enums.ParseMode.Html
                );

                foreach (var toDoList in toDoLists.OrderBy(x => x.Name))
                {
                    text = "";

                    int completed = 0;
                    int InProgress = 0;
                    int overdue = 0;

                    var cases = await _caseService.GetCases(Guid.Parse(toDoList.Id), user);

                    foreach (var @case in cases)
                    {
                        if (@case.Status)
                            completed++;
                        else
                        {
                            if (@case.DateEnd < DateTime.Now)
                                overdue++;
                            else
                                InProgress++;
                        }
                    }
                    text +=
                        $"━━━━━━━━━━━━━━━\n"+
                        $"{localization["Title"]}: <i>{toDoList.Name}</i>\n" +
                        $"{localization["Goals"]}: <i>{cases.Count}</i>\n" +
                        $"{localization["Completed"]}: <i>{completed}</i>\n" +
                        $"{localization["Progress"]}: <i>{InProgress}</i>\n" +
                        $"{localization["Overdue"]}: <i>{overdue}</i>\n" +
                        $"━━━━━━━━━━━━━━━\n";

                    await _bot.SendMessage(
                        chatId: chatId,
                        text: text,
                        replyMarkup: _telegramKeyboardService.GetSettingToDoListKeyboard(userId,toDoList.Id.ToString()),
                        parseMode: Telegram.Bot.Types.Enums.ParseMode.Html
                    );
                }
            }
        }

        public async Task SendCasesMesage(User user, long chatId, string nameToDoList, ICollection<ReturnCaseDTO> cases,string idToDoList)
        {
            var userId = user.Id;

            var localization = new ResxLocalizer(_sessionService.GetLanguage(userId));

            if (cases.Count == 0)
            {
                await _bot.SendMessage(
                    chatId: chatId,
                    text: localization["RequestCreateCases"]
                    //replyMarkup: _telegramKeyboardService.GetAddCaseMenuKeyboard(userId)
                );
            }
            else
            {
                string text =
                        $"{localization["Title"]}: <i>{nameToDoList}</i>\n" +
                        $"\n";

                await _bot.SendMessage(
                    chatId: chatId,
                    text: text,
                    replyMarkup: _telegramKeyboardService.GetMenuKeyboard(userId),
                    parseMode: Telegram.Bot.Types.Enums.ParseMode.Html
                );

                foreach (var @case in cases.OrderBy(x => x.Name))
                {
                    text = "";

                    var stsus = @case.Status
                        ? localization["StatusCompleted"]
                        : localization["StatusNotCompleted"];

                    if (@case.DateEnd < DateTime.Now)
                        stsus = localization["StatusOverdue"];
                    text +=
                    $"━━━━━━━━━━━━━━━\n" +
                    $"{localization["Title"]}: <i>{@case.Name}</i>\n" +
                    $"{localization["EndDate"]}: <i>{@case.DateEnd:dd.MM.yyyy}</i>\n" +
                    $"{localization["Status"]}: <i>{stsus}</i>\n" +
                    $"━━━━━━━━━━━━━━━\n";

                    await _bot.SendMessage(
                        chatId: chatId,
                        text: text,
                        replyMarkup: @case.Status? _telegramKeyboardService.GetSettingTrueCaseKeyboard(userId, @case.Id.ToString()) : _telegramKeyboardService.GetSettingCaseKeyboard(userId, @case.Id.ToString(),idToDoList),
                        parseMode: Telegram.Bot.Types.Enums.ParseMode.Html
                    );
                }
            }
        }

        public async Task SendListAddedSuccessMessage(long userId, long chatId)
        {
            var localization = new ResxLocalizer(_sessionService.GetLanguage(userId));

            await _bot.SendMessage(
                chatId: chatId,
                text: localization["ListAddedSuccess"]
            );
        }

        public async Task SendListDeleteedSuccessMessage(long userId, long chatId)
        {
            var localization = new ResxLocalizer(_sessionService.GetLanguage(userId));

            await _bot.SendMessage(
                chatId: chatId,
                text: localization["ListDeleteedSuccess"]
            );
        }

        public async Task SendDeleteCaseEnterMessage(long userId, long chatId)
        {
            var localization = new ResxLocalizer(_sessionService.GetLanguage(userId));

            await _bot.SendMessage(
                chatId: chatId,
                text: localization["CaseDeleteedSuccess"]
            );
        }

        public async Task SendDeleteListEnterMessage(long userId, long chatId)
        {
            var localization = new ResxLocalizer(_sessionService.GetLanguage(userId));

            await _bot.SendMessage(
                chatId: chatId,
                text: localization["DeleteListEnterNamePrompt"]
            );
        }

        public async Task SendAddListEnterMessage(long userId, long chatId)
        {
            var localization = new ResxLocalizer(_sessionService.GetLanguage(userId));

            await _bot.SendMessage(
                chatId: chatId,
                text: localization["AddListEnterNamePrompt"]
            );
        }

        public async Task SendAddCaseEnterMessage(long userId, long chatId)
        {
            var localization = new ResxLocalizer(_sessionService.GetLanguage(userId));

            await _bot.SendMessage(
                chatId: chatId,
                text: localization["AddCaseEnterNamePrompt"]
            );
        }

        public async Task SendEditListEnterMessage(long userId, long chatId)
        {
            var localization = new ResxLocalizer(_sessionService.GetLanguage(userId));

            await _bot.SendMessage(
                chatId: chatId,
                text: localization["EditListEnterNamePrompt"]
            );
        }

        public async Task SendHelpMessage(long userId, long chatId)
        {
            var localization = new ResxLocalizer(_sessionService.GetLanguage(userId));

            await _bot.SendMessage(
                chatId: chatId,
                text: localization["Help"],
                replyMarkup: _telegramKeyboardService.GetMainMenuKeyboard(userId)
            );
        }

        public async Task SendSelectLanguageMessage(long userId, long chatId)
        {
            var localization = new ResxLocalizer(_sessionService.GetLanguage(userId));

            await _bot.SendMessage(
                chatId: chatId,
                text: localization["SelectLanguage"]
            );
        }

        public async Task SendLanguageSetConfirmationMessage(long userId, long chatId)
        {
            var localization = new ResxLocalizer(_sessionService.GetLanguage(userId));

            await _bot.SendMessage(
                chatId: chatId,
                text: localization["LanguageSetConfirmation"],
                replyMarkup: _telegramKeyboardService.GetMenuKeyboard(userId)
            );
        }

        public async Task SendSelectLanguageKeyboardMessage(long userId, long chatId)
        {
            var localization = new ResxLocalizer(_sessionService.GetLanguage(userId));

            await _bot.SendMessage(
                chatId: chatId,
                text: localization["SelectLanguage"],
                replyMarkup: _telegramKeyboardService.GetLanguage(userId)
            );
        }

        public async Task SendMenuMessage(long userId, long chatId)
        {
            var localization = new ResxLocalizer(_sessionService.GetLanguage(userId));

            await _bot.SendMessage(
                chatId: chatId,
                text: localization["Menu"],
                replyMarkup: _telegramKeyboardService.GetMainMenuKeyboard(userId)
            );
        }

        public async Task SendWhatWouldYouChangeMessage(long userId, long chatId)
        {
            var localization = new ResxLocalizer(_sessionService.GetLanguage(userId));

            await _bot.SendMessage(
                chatId: chatId,
                text: localization["WhatWouldYouChange"],
                replyMarkup: _telegramKeyboardService.GetLanguageKeyboard(userId)
            );
        }

        public async Task SendToDoListAlreadyExistsMessage(long userId, long chatId)
        {
            var localization = new ResxLocalizer(_sessionService.GetLanguage(userId));

            await _bot.SendMessage(
                chatId: chatId,
                text: localization["ToDoListAlreadyExists"]
            );
        }

        public async Task SendDateCaseEnter(long userId, long chatId)
        {
            var localization = new ResxLocalizer(_sessionService.GetLanguage(userId));

            await _bot.SendMessage(
                chatId: chatId,
                text: localization["EnterTaskDueDate"]
            );
        }

        public async Task SendTrueCreateCase(long userId, long chatId)
        {
            var localization = new ResxLocalizer(_sessionService.GetLanguage(userId));

            await _bot.SendMessage(
                chatId: chatId,
                text: localization["TaskCreatedSuccessfully"]
            );
        }

        public async Task SendTrueGetAwardCase(long userId, long chatId)
        {
            var localization = new ResxLocalizer(_sessionService.GetLanguage(userId));

            await _bot.SendMessage(
                chatId: chatId,
                text: localization["AwardGranted"]
            );
        }

        public async Task SendNoAwardsMessage(long userId, long chatId)
        {
            var localization = new ResxLocalizer(_sessionService.GetLanguage(userId));

            await _bot.SendMessage(
                chatId: chatId,
                text: localization["NoAwardsText"],
                replyMarkup: _telegramKeyboardService.GetMenuKeyboard(userId)
            );
        }

        public async Task SendAwardsMessage(long userId, long chatId,ICollection<ReturnAwardDTO> awards)
        {
            var localization = new ResxLocalizer(_sessionService.GetLanguage(userId));

            await _bot.SendMessage(
                chatId: chatId,
                text: localization["AwardsText"],
                replyMarkup: _telegramKeyboardService.GetMenuKeyboard(userId)
            );

            string text = "";

            foreach (var award in awards)
            {
                text += award.Name + "\n━━━━━━━━━━━━━━━\n";
            }

            await _bot.SendMessage(
                chatId: chatId,
                text: text
            );
        }

    }
}
