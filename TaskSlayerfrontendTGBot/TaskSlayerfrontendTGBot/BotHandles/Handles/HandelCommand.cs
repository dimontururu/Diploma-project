using System.Net.Http.Headers;
using TaskSlayerfrontendTGBot.ApiClient;
using TaskSlayerfrontendTGBot.BotHandles.IHandles;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;


namespace TaskSlayerfrontendTGBot.BotHandles.Handles
{
    internal class HandelCommand : IHandelCommand
    {
        private readonly ITelegramBotClient _botClient;
        private readonly TaskServiceApiClient _Api;
        public HandelCommand(ITelegramBotClient botClient, TaskServiceApiClient Api)
        {
            _botClient = botClient;
            _Api =  Api;
        }
        public async Task StartCommand(ITelegramBotClient bot, Update update, CancellationToken cancellationToken)
        {
            var message = update.Message;
            var chatId = message.Chat.Id;
            var user = message.From;

            string welcomeText = $"Привет, {user?.FirstName ?? "друг"}! 👋\n" +
                $"🎮 Добро пожаловать в TaskSlayer — твой игровой Todo-лист!\n\n" +
                $"Ты — герой в мире задач, и каждый чекбокс — это победа над хаосом! 🌟\n\n" +
                $"📜 Как это работает?\n\n" +
                $"Добавляй задачи, как квесты.\n\n" +
                $"Отмечай выполненные задачи — получай опыт и уровни.\n\n" +
                $"Зарабатывай достижения и сражайся с прокрастинацией!\n\n" +
                $"⚔️ Твои инструменты:\n" +
                $"✅ /To_do_list — посмотреть списки\n" +
                $"📋 /list — показать активные задания\n" +
                $"🏆 /stats — твой прогресс и награды\n" +
                $"🎯 /goals — долгосрочные миссии\n" +
                $"💙/help — Не бойся просить помощи!\n\n" +
                $"🔥 Готов стать легендой продуктивности? Тогда в бой!";

            var replyKeyboard = new ReplyKeyboardMarkup(new[]
            {
                new KeyboardButton[] { "/To_do_list", "Кнопка 2" },
                new KeyboardButton[] { "Кнопка 3" }
            })
            {
                ResizeKeyboard = true
            };

            await bot.SendMessage(
                chatId: chatId,
                text: welcomeText,
                cancellationToken: cancellationToken,
                replyMarkup: replyKeyboard
            );
            var userDTO = new UserDTO
                { 
                    Name = user.Username,
                    Id = user.Id.ToString(),
                    Type_id = "Telegram"
                };

            Console.WriteLine($"Создание иди получение существующего пользователя\n Name = {user.Username}\nId = {user.Id.ToString()}\nType_id = \"Telegram\"");

            await _Api.CreateUserAsync(userDTO);
        }

        public async Task To_do_listCommand(ITelegramBotClient bot, Update update, CancellationToken cancellationToken)
        {
            var message = update.Message;
            var chatId = message.Chat.Id;
            var user = message.From;

            string Text = "✨📋 Твои списки задач 📋✨";

            var inlineKeyboard = new InlineKeyboardMarkup(new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Добавить список", "add/ToDoList/"+user.Id.ToString()+":"+""),
                    }
                });

            await bot.SendMessage(
                chatId: chatId,
                text: Text,
                cancellationToken: cancellationToken,
                replyMarkup: inlineKeyboard
            );

            var userDTO = new UserDTO
            {
                Name = user.Username,
                Id = user.Id.ToString(),
                Type_id = "Telegram"
            };

            _Api._httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", await _Api.AuthorizationAsync(userDTO));

            ICollection<ReturnToDoListsDTO> ToDoListsDTOs = await _Api.GetToDoListsAsync();

            if (ToDoListsDTOs.Count == 0)
            {
                await bot.SendMessage(
                    chatId: chatId,
                    text: "🚫 списков нет…\n✨ <i>Но никогда не поздно начать!</i>\n➕ Добавь первый прямо сейчас!",
                    cancellationToken: cancellationToken,
                    parseMode: Telegram.Bot.Types.Enums.ParseMode.Html
                );
            }
            foreach ( var toDo in ToDoListsDTOs )
            {
                inlineKeyboard = new InlineKeyboardMarkup(new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Просмотреть", "view/ToDoList/"+user.Id.ToString()+":"+toDo.Id),
                        InlineKeyboardButton.WithCallbackData("Изменить", "change/ToDoList/"+user.Id.ToString()+":"+toDo.Id),
                        InlineKeyboardButton.WithCallbackData("Удалить", "delete/ToDoList/"+user.Id.ToString()+":"+toDo.Id)
                    }
                });

                Text = "📝 " + toDo.Name;
                await bot.SendMessage(
                    chatId: chatId,
                    text: Text,
                    cancellationToken: cancellationToken,
                    replyMarkup: inlineKeyboard
                );
            }
        }
    }
}
