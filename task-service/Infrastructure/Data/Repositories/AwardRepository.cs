using Microsoft.EntityFrameworkCore;
using task_service.Domain.Entities;
using task_service.Domain.Interfaces.IRepository;

namespace task_service.Infrastructure.Data.Repositories
{
    internal class AwardRepository : IAwardRepository
    {
        private readonly ToDoListContext _DB;
        public AwardRepository(ToDoListContext DB) 
        { 
            _DB = DB;
        }
        public async Task<ICollection<Award>> GetAllAsync()
        {
            return await _DB.Awards.ToListAsync();
        }

        public async Task<Award> GetAsync(Guid id)
        {
            return await _DB.Awards.FindAsync(id);
        }
    }
}
