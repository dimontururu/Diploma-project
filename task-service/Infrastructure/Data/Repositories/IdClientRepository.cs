using Microsoft.EntityFrameworkCore;
using task_service.Domain.Entities;
using task_service.Domain.Interfaces.IRepository;

namespace task_service.Infrastructure.Data.Repositories
{
    public class IdClientRepository : IIdClientRepository
    {
        private readonly ToDoListContext _DB;

        public IdClientRepository(ToDoListContext DB)
        {
            _DB = DB;
        }
        public async Task AddAsync(IdClient idClient)
        {
            await _DB.IdClients.AddAsync(idClient);
            await _DB.SaveChangesAsync();
        }

        public async Task<IdClient> GetAsync(Guid IdClientType, string IdClient)
        {
            return await _DB.IdClients
                .Include(ic => ic.IdClientTypeNavigation)
                .Include(ic => ic.IdUserNavigation)
                .FirstOrDefaultAsync(ic => ic.IdClient1 == IdClient && ic.IdClientType == IdClientType);
        }
    }
}
