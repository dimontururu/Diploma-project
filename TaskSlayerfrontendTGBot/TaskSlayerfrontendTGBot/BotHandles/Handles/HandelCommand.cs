using TaskSlayerfrontendTGBot.ApiClient;
using TaskSlayerfrontendTGBot.BotHandles.IHandles;
using Telegram.Bot;
using Telegram.Bot.Types;


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
                $"✅ /add — добавить квест\n" +
                $"📋 /list — показать активные задания\n" +
                $"🏆 /stats — твой прогресс и награды\n" +
                $"🎯 /goals — долгосрочные миссии\n" +
                $"💙/help — Не бойся просить помощи!\n\n" +
                $"🔥 Готов стать легендой продуктивности? Тогда в бой!";

            await bot.SendMessage(
                chatId: chatId,
                text: welcomeText,
                cancellationToken: cancellationToken
            );
            var userDTO = new UserDTO
                { 
                    Name = user.Username,
                    Id = user.Id.ToString(),
                    Type_id = "Telegram"
                };

            Console.WriteLine($"Создание иди получение существующего пользователя\n Name = {user.Username}\nId = {user.Id.ToString()}\nType_id = \"Telegram\"");

            var a = await _Api.CreateUserAsync(userDTO);
            Console.WriteLine("token:" + a);
        }
    }
}
