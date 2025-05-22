using task_service.Domain.Entities;
using task_service.Domain.Interfaces;
using task_service.Infrastructure.Data;

namespace Infrastructure.Data.Repositories
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
    }
}
