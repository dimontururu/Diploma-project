using Application.Interfaces.Message;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Presentation.Bot.Handlers
{
    internal class BotAddedToChatHandler : IMessageHandler
    {
        public readonly ITelegramBotClient _bot;

        public BotAddedToChatHandler(ITelegramBotClient bot)
        {
            _bot = bot;
        }
        public async Task<bool> CanHandle(Update update)
        {
            if (update.Type == UpdateType.MyChatMember &&
                update.MyChatMember.NewChatMember.User.IsBot &&
                update.MyChatMember.NewChatMember.Status == ChatMemberStatus.Member)
                return true;
            return false;
        }

        public async Task<bool> CanHandle(string State, Update update)
        {
            return false;
        }

        public async Task HandleAsync(Update update)
        {
            var chatId = update.MyChatMember.Chat.Id;

            try
            {
                await _bot.LeaveChat(chatId);
            }
            catch { }
            return;
        }
    }
}
