using Application.DTOs;
using task_service.Application.DTOs;
using task_service.Domain.Entities;

namespace task_service.Application.Interfaces.Services
{
    public interface IToDoListService
    {
        Task<ToDoList> CreateToDoList(NewToDoListDTO toDoListDTO,User user);

        Task<ICollection<ReturnToDoListsDTO>> GetToDoLists(User user);

        Task DeleteToDoList(Guid id, User user);
        Task PutToDoList(PutToDoListDTO putToDoListDTO, User user);
    }
}
