using Telegram.Bot.Types;

namespace Application.Interfaces.Message
{
    public interface IStatefulMessageHandle:IMessageHandler
    {
        Task<bool> CanHandle(string state, Update update);
    }
}
