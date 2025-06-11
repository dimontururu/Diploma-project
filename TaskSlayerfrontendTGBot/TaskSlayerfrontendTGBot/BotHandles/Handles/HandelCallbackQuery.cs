//using System.Net.Http.Headers;
//using TaskSlayerfrontendTGBot.ApiClient;
//using TaskSlayerfrontendTGBot.BotHandles.IHandles;
//using TaskSlayerfrontendTGBot.Interface.IServices;
//using Telegram.Bot;
//using Telegram.Bot.Types;
//using Telegram.Bot.Types.ReplyMarkups;
//using static System.Net.Mime.MediaTypeNames;


//namespace TaskSlayerfrontendTGBot.BotHandles.Handles
//{
//    internal class HandelCallbackQuery : IHandelCallbackQuery
//    {

//        private readonly ITelegramBotClient _botClient;
//        private readonly TaskServiceApiClient _Api;
//        private readonly IUserStateService _userStateService;

//        public HandelCallbackQuery(ITelegramBotClient botClient, TaskServiceApiClient Api, IUserStateService userStateService)
//        {
//            _botClient = botClient;
//            _Api = Api;
//            _userStateService = userStateService;
//        }
//        public async Task HandelCallbackQueryAsync(ITelegramBotClient bot, Update update, CancellationToken cancellationToken)
//        {
//            var callbackData = update.CallbackQuery.Data;
//            var callbackQueryId = update.CallbackQuery.Id;
//            var chatCallbackQueryId = update.CallbackQuery.Message.Chat.Id;

//            int firstSlash = callbackData.IndexOf('/');
//            int secondSlash = callbackData.IndexOf('/', firstSlash + 1);
//            int colonIndex = callbackData.IndexOf(':');


//            string action = callbackData.Substring(0, firstSlash);
//            string obj = callbackData.Substring(firstSlash + 1, secondSlash - firstSlash - 1);
//            string idObj = callbackData.Substring(secondSlash + 1, colonIndex - secondSlash - 1);
//            string idTarget = callbackData.Substring(colonIndex + 1);

//            var userDTO = new UserDTO
//            {
//                Name = "Костыль",
//                Id = idObj,
//                Type_id = "Telegram"
//            };

//            _Api._httpClient.DefaultRequestHeaders.Authorization =
//                new AuthenticationHeaderValue("Bearer", await _Api.AuthorizationAsync(userDTO));

//            switch (action)
//            {
//                case "delete":
//                    switch (obj)
//                    {
//                        case "ToDoList":
//                            await _Api.DeleteToDoListAsync(Guid.Parse(idTarget));
//                            await bot.SendMessage(chatCallbackQueryId, "✅ " + "Список успешно удалён");
//                            break;
//                        case "Case":
//                            await _Api.DeleteCaseAsync(Guid.Parse(idTarget));
//                            await bot.SendMessage(chatCallbackQueryId, "✅ " + "Задача успешно удалена");
//                            break;
//                    }
//                    break;
//                case "view":
//                    switch (obj)
//                    {
//                        case "ToDoList":
//                            ICollection<ReturnCaseDTO> CaseDTOs = await _Api.GetCasesAsync(Guid.Parse(idTarget));

//                            var inlineKeyboard = new Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup(new[]
//                                {
//                                    new[]
//                                    {
//                                        InlineKeyboardButton.WithCallbackData("Добавить цель", "add/Case/"+idObj.ToString()+":"+idTarget),
//                                    }
//                                });

//                            await bot.SendMessage(
//                                    chatId: chatCallbackQueryId,
//                                    text: "🚀 <b>Твои цели</b>",
//                                    cancellationToken: cancellationToken,
//                                    replyMarkup: inlineKeyboard,
//                                    parseMode: Telegram.Bot.Types.Enums.ParseMode.Html
//                                );

//                            bool thereTasks = false;
//                            foreach (var CaseDTO in CaseDTOs)
//                            {
//                                thereTasks = true;

//                                inlineKeyboard = new Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup(new[]
//                                {
//                                    new[]
//                                    {
//                                        InlineKeyboardButton.WithCallbackData("Изменить", "change/Case/"+idObj.ToString()+":"+CaseDTO.Id),
//                                        InlineKeyboardButton.WithCallbackData("Удалить", "delete/Case/"+idObj.ToString()+":"+CaseDTO.Id)
//                                    }
//                                });

//                                string Text = $"📝 <b>{CaseDTO.Name}</b>\n" +
//                                     $"📅 До: <i>{CaseDTO.DateEnd:dd.MM.yyyy}</i>\n" +
//                                     $"Статус: {(CaseDTO.Status ? "✅ Выполнено" : "❌ Не выполнено")}";

//                                await bot.SendMessage(
//                                    chatId: chatCallbackQueryId,
//                                    text: Text,
//                                    cancellationToken: cancellationToken,
//                                    replyMarkup: inlineKeyboard,
//                                    parseMode: Telegram.Bot.Types.Enums.ParseMode.Html
//                                );
//                            }

//                            if (!thereTasks)
//                            {
//                                await bot.SendMessage(
//                                    chatId: chatCallbackQueryId,
//                                    text: "🚫 Целей нет…\n✨ <i>Но никогда не поздно начать!</i>\n➕ Добавь первую прямо сейчас!",
//                                    cancellationToken: cancellationToken,
//                                    parseMode: Telegram.Bot.Types.Enums.ParseMode.Html
//                                );
//                            }
//                            break;
//                    }
//                    break;
//                case "change":
//                    switch (obj)
//                    {
//                        case "ToDoList":
//                            await bot.SendMessage(chatCallbackQueryId, "📋 <b>Как назвать этот список?</b>\n✏️ Введите новое имя ниже:", Telegram.Bot.Types.Enums.ParseMode.Html);
//                            _userStateService.SetState(chatCallbackQueryId, $"change/ToDoList/name:{idTarget}");
//                            break;
//                    }
//                    break;
//                case "add":
//                    switch(obj)
//                    {
//                        case "ToDoList":
//                            await bot.SendMessage(chatCallbackQueryId, "📋 <b>Как назвать этот список?</b>\n✏️ Введите название ниже:", Telegram.Bot.Types.Enums.ParseMode.Html);
//                            _userStateService.SetState(chatCallbackQueryId, $"add/ToDoList/name:{idTarget}");
//                            break;
//                        case "Case":
//                            await bot.SendMessage(chatCallbackQueryId, "📋 <b>Как назвать этоу задачу?</b>\n✏️ Введите название ниже:", Telegram.Bot.Types.Enums.ParseMode.Html);
//                            _userStateService.SetState(chatCallbackQueryId, $"add/Case/name:{idTarget}");
//                            break;
//                    }
//                    break;
//            }

            
//        }
//    }
//}
