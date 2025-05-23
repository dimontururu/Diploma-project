using task_service.Domain.Interfaces.IRepository;
using task_service.Domain.Entities;

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
    }
}
