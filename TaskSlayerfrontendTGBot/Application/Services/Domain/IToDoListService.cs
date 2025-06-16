using Domain.DTOs.ToDoList;
using Telegram.Bot.Types;

namespace Application.Services.Domain
{
    public interface IToDoListService
    {
        Task CreateToDoList(string nameToDoList, User user);
        Task<ICollection<ReturnToDoListsDTO>> GetToDoLists(User user);
        Task DeleteToDoList(Guid IdToDoList, User user);
        Task EditToDoList(PutToDoListDTO putToDoListDTO, User user);
    }
}
