using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;
using task_service.Domain.Entities;
using task_service.Domain.Interfaces;

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
            return await _DB.ClientTypes.FirstOrDefaultAsync(ct => ct.Type == typeId);
        }
    }
}
