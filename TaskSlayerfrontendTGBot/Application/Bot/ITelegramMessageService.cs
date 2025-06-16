using Domain.DTOs.Award;
using Domain.DTOs.Case;
using Domain.DTOs.ToDoList;
using Telegram.Bot.Types;

namespace Application.Bot
{
    public interface ITelegramMessageService
    {
        Task SendWelcomeMessage(long userId, long chatId);
        Task SendToDoMessage(User user, long chatId, ICollection<ReturnToDoListsDTO> toDoLists);
        Task SendListAddedSuccessMessage(long userId, long chatId);
        Task SendAddListEnterMessage(long userId, long chatId);
        Task SendHelpMessage(long userId, long chatId);
        Task SendSelectLanguageMessage(long userId, long chatId);
        Task SendLanguageSetConfirmationMessage(long userId, long chatId);
        Task SendSelectLanguageKeyboardMessage(long userId, long chatId);
        Task SendMenuMessage(long userId, long chatId);
        Task SendWhatWouldYouChangeMessage(long userId, long chatId);
        Task SendDeleteListEnterMessage(long userId, long chatId);
        Task SendListDeleteedSuccessMessage(long userId, long chatId);
        Task SendToDoListAlreadyExistsMessage(long userId, long chatId);
        Task SendEditListEnterMessage(long userId, long chatId);
        Task SendCasesMesage(User user, long chatId, string nameToDoList, ICollection<ReturnCaseDTO> cases, string idToDoList);
        Task SendAddCaseEnterMessage(long userId, long chatId);
        Task SendDateCaseEnter(long userId, long chatId);
        Task SendTrueCreateCase(long userId, long chatId);
        Task SendDeleteCaseEnterMessage(long userId, long chatId);
        Task SendTrueGetAwardCase(long userId, long chatId);
        Task SendNoAwardsMessage(long userId, long chatId);
        Task SendAwardsMessage(long userId, long chatId, ICollection<ReturnAwardDTO> awards);
    }
}
