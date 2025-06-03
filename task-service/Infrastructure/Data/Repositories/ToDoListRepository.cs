using Microsoft.EntityFrameworkCore;
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

        public async Task DeleteAsync(Guid id)
        {
            _DB.ToDoLists.Remove(await GetAsync(id));
            await _DB.SaveChangesAsync();
        }

        public async Task<ToDoList> GetAsync(Guid id)
        {
            return await _DB.ToDoLists
                .Include(t => t.IdUserNavigation)
                .Include(t => t.cases)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task PutAsync(ToDoList toDoList)
        {
            _DB.Entry(toDoList).State = EntityState.Modified;
            await _DB.SaveChangesAsync();
        }
    }
}
