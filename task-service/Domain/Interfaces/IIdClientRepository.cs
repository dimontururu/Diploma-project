using task_service.Domain.Entities;

namespace task_service.Domain.Interfaces
{
    public interface IIdClientRepository
    {
        Task AddAsync(IdClient idClient);
    }
}
