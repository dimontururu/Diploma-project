using Telegram.Bot.Types;

namespace Application.Services.Domain
{
    public interface IUserErrorService
    {
        Task SendInvalidNameError(long userId, long chatId);
        Task ListLimitError(long userId, long chatId);
        Task SendDateError(long userId, long chatId);
    }
}
