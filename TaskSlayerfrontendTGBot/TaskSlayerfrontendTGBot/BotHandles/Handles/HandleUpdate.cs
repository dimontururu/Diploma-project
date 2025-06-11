//using TaskSlayerfrontendTGBot.BotHandles.IHandles;
//using TaskSlayerfrontendTGBot.Interface.IServices;
//using Telegram.Bot;
//using Telegram.Bot.Types;
//using Telegram.Bot.Types.Enums;

//namespace TaskSlayerfrontendTGBot.BotHandles.Handles
//{
//    internal class HandleUpdate:IHandleUpdate
//    {
//        private readonly IHandelCommand _handelCommand;
//        private readonly IHandelCallbackQuery _handelCallbackQuery;
//        private readonly IUserStateService _userStateService;
//        private readonly IHandleUserState _handleUserState;
//        public HandleUpdate(IHandelCommand handelCommand, IHandelCallbackQuery handelCallbackQuery, IUserStateService userStateService, IHandleUserState handleUserState) 
//        {
//            _handelCommand = handelCommand;
//            _handelCallbackQuery = handelCallbackQuery;
//            _userStateService = userStateService;
//            _handleUserState = handleUserState; 
//        }
//        public async Task HandleUpdateAsync(ITelegramBotClient bot, Update update, CancellationToken cancellationToken)
//        {
//            if (update.Type == UpdateType.CallbackQuery && update.CallbackQuery != null)
//            {
//                if (_userStateService.GetState(update.CallbackQuery.Message.Chat.Id) == null)
//                {
//                    await _handelCallbackQuery.HandelCallbackQueryAsync(bot, update, cancellationToken);
                    
//                }
//                else
//                {
//                    await bot.SendMessage(update.CallbackQuery.Message.Chat.Id, "<b>Не балуйся</b>", Telegram.Bot.Types.Enums.ParseMode.Html);
//                    _userStateService.ClearState(update.CallbackQuery.Message.Chat.Id);
//                }
//                return;
//            }

//            var message = update.Message;
//            var chatId = message.Chat.Id;
//            var user = message.From;

//            Console.WriteLine($"[{user?.Id}] {user?.FirstName}: {message.Text ?? message.Type.ToString()}");

//            var state = _userStateService.GetState(chatId);
//            if (state != null)
//            {
//                await _handleUserState.HandleUserStateAsync(bot, update, cancellationToken);
//                return;
//            }

//            if (message.Text == "/start")
//            {
//                await _handelCommand.StartCommand(bot, update, cancellationToken);
//                return;
//            }

//            if (message.Text == "/To_do_list")
//            {
//                await _handelCommand.To_do_listCommand(bot, update, cancellationToken);
//                return;
//            }

//            string welcomeText = $"/help — Не бойся просить помощи! 💙";

//            await bot.SendMessage(
//                chatId: chatId,
//                text: welcomeText,
//                cancellationToken: cancellationToken
//            );
//            return;
//        }
//    }
//}
