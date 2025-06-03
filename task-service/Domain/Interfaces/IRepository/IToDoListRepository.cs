using task_service.Domain.Entities;

namespace task_service.Domain.Interfaces.IRepository
{
    public interface IToDoListRepository
    {
        Task AddAsync(ToDoList toDoList);
        Task<ToDoList> GetAsync(Guid id);
    }
}
