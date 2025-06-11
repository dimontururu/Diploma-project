using Telegram.Bot.Types;

namespace Application.Interfaces.Message
{
    public interface IMessageHandler
    {
        Task<bool> CanHandle(Update update);
        Task HandleAsync(Update update);
    }
}
