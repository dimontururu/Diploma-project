using task_service.Domain.Entities;

namespace task_service.Domain.Interfaces.IRepository
{
    public interface IIdClientRepository
    {
        Task AddAsync(IdClient idClient);

        Task<IdClient> GetAsync(Guid IdClientType, string IdClient);
    }
}
