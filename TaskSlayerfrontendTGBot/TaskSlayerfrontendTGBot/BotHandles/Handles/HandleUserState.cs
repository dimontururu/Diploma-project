//using Newtonsoft.Json;
//using System.Globalization;
//using System.Net.Http.Headers;
//using TaskSlayerfrontendTGBot.ApiClient;
//using TaskSlayerfrontendTGBot.BotHandles.IHandles;
//using TaskSlayerfrontendTGBot.Interface.IServices;
//using Telegram.Bot;
//using Telegram.Bot.Types;

//namespace TaskSlayerfrontendTGBot.BotHandles.Handles
//{
//    internal class HandleUserState : IHandleUserState
//    {
//        IUserStateService _userStateService;
//        private readonly TaskServiceApiClient _Api;
//        public HandleUserState(IUserStateService userStateService, TaskServiceApiClient Api) 
//        { 
//            _userStateService = userStateService;
//            _Api = Api;
//        }
//        public async Task HandleUserStateAsync(ITelegramBotClient bot, Update update, CancellationToken cancellationToken)
//        {
//            var message = update.Message;
//            var chatId = message.Chat.Id;
//            var user = message.From;

//            var state = _userStateService.GetState(chatId);

//            var parts = state.Split(':');
//            string leftPart = parts[0];
//            string idTarget = parts[1];

//            var segments = leftPart.Split('/');
//            string action = segments[0];
//            string obj = segments[1];
//            string property = segments[2];

//            var userDTO = new UserDTO
//            {
//                Name = "Костыль",
//                Id = user.Id.ToString(),
//                Type_id = "Telegram"
//            };

//            _Api._httpClient.DefaultRequestHeaders.Authorization =
//                new AuthenticationHeaderValue("Bearer", await _Api.AuthorizationAsync(userDTO));

//            switch (obj)
//            {
//                case "ToDoList":
//                    switch(action)
//                    {
//                        case "change":
//                            string userMessagehange = update.Message.Text;
                             
//                            PutToDoListDTO putToDoListDTOChange = new PutToDoListDTO
//                            {
//                                Name = userMessagehange,
//                                Id = Guid.Parse(idTarget)
//                            };
//                            await _Api.PutToDoListAsync(putToDoListDTOChange);

//                            await bot.SendMessage(chatId, "✅ " + "Список успешно изменён", Telegram.Bot.Types.Enums.ParseMode.Html);
//                            break;
//                        case "add":
//                            string userMessageAdd = update.Message.Text;

//                            NewToDoListDTO newToDoListDTOAdd = new NewToDoListDTO
//                            {
//                                Name = userMessageAdd,
                                
//                            };
//                            await _Api.CreateTodolistAsync(newToDoListDTOAdd);

//                            await bot.SendMessage(chatId, "✅ " + "Список успешно создан", Telegram.Bot.Types.Enums.ParseMode.Html);
//                            break;
//                    }
//                    break;
//                case "Case":
//                    if (segments.Length == 3)
//                    {
//                        await bot.SendMessage(
//                            chatId,
//                            "🗓️ <b>Когда ты хочешь завершить задачу?</b>\n✍️ Введи дату в формате <i>ДД.ММ.ГГГГ</i>",
//                            Telegram.Bot.Types.Enums.ParseMode.Html
//                        );

//                        _userStateService.ClearState(chatId);
//                        _userStateService.SetState(chatId, $"add/Case/name/"+ update.Message.Text + $":{idTarget}");

//                        return;
//                    }
//                    else
//                    {
//                        if (DateTime.TryParseExact(update.Message.Text, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out DateTime parsedDate))
//                        {
//                            var dateEnd = new DateTimeOffset(parsedDate, TimeSpan.Zero); // гарантированно UTC

//                            NewCaseDTO newCaseDTO = new NewCaseDTO
//                            {
//                                Name = segments[3],
//                                Id_to_do_list = Guid.Parse(idTarget),
//                                DateEnd = dateEnd,
//                                DateOfCreation = DateTimeOffset.UtcNow
//                            };
                            
//                            await _Api.CreateCaseAsync(Guid.Parse(idTarget), newCaseDTO);

//                            await bot.SendMessage(chatId, "✅ " + "Задача успешно создана", Telegram.Bot.Types.Enums.ParseMode.Html);
//                        }
//                        else
//                        {
//                            await bot.SendMessage(chatId, "<b>Не балуйся</b>", Telegram.Bot.Types.Enums.ParseMode.Html);
//                        }
//                    }
//                break;
//            }

//            _userStateService.ClearState(chatId);
//        }
//    }
//}
