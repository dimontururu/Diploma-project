using task_service.Domain.Entities;
using task_service.Domain.Interfaces.IRepository;

namespace task_service.Infrastructure.Data.Repositories
{
    public class ToDoListRepository : IToDoListRepository
    {
        private readonly ToDoListContext _DB;

        public ToDoListRepository(ToDoListContext dB)
        {
            _DB = dB;
        }

        public async Task AddAsync(ToDoList toDoList)
        {
            await _DB.ToDoLists.AddAsync(toDoList);
            await _DB.SaveChangesAsync();
        }

        public async Task<ToDoList> GetAsync(Guid id)
        {
            return await _DB.ToDoLists.FindAsync(id);
        }
    }
}
