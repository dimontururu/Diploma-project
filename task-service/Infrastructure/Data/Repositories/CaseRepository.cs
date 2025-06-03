using Microsoft.EntityFrameworkCore;
using task_service.Domain.Entities;
using task_service.Domain.Interfaces.IRepository;

namespace task_service.Infrastructure.Data.Repositories
{
    public class CaseRepository : ICaseRepository
    {
        ToDoListContext _DB;
        public CaseRepository(ToDoListContext DB) 
        {
            _DB = DB;
        }
        public async Task DeleteAsync(Guid id)
        {
            _DB.Cases.Remove(await GetAsync(id));
            await _DB.SaveChangesAsync();
        }

        public async Task<Case> GetAsync(Guid id)
        {
            return await _DB.Cases
                .Include(C => C.id_to_do_listNavigation)
                .FirstOrDefaultAsync(C => C.Id == id);
        }
    }
}
