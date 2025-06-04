using task_service.Application.DTOs.ToDoListDTO;
using task_service.Domain.Entities;

namespace task_service.Application.Interfaces.Services
{
    public interface IToDoListService
    {
        Task<ToDoList> CreateToDoList(NewToDoListDTO toDoListDTO,User user);

        Task<ICollection<ReturnToDoListsDTO>> GetToDoLists(User user);

        Task DeleteToDoList(Guid id);
        Task PutToDoList(PutToDoListDTO putToDoListDTO);

        Task<ToDoList> GetToDoList(Guid id);
    }
}
