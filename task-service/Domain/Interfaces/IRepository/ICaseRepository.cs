using task_service.Domain.Entities;

namespace task_service.Domain.Interfaces.IRepository
{
    public interface ICaseRepository
    {
        Task AddAsync(Case @case);
        Task<Case> GetAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task PutAsync(Case @case);
    }
}
