using task_service.Domain.Interfaces.IRepository;
using Microsoft.EntityFrameworkCore;
using task_service.Domain.Entities;

namespace task_service.Infrastructure.Data.Repositories
{
    public class ClientTypeRepository : IClientTypeRepository
    {
        private readonly ToDoListContext _DB;

        public ClientTypeRepository(ToDoListContext DB)
        {
            _DB = DB;
        }

        public async Task<ClientType?> GetByTypeAsync(string typeId)
        {
            return await _DB.ClientTypes
                .Include(ct => ct.IdClients)
                .FirstOrDefaultAsync(ct => ct.Type == typeId);
        }
    }
}
