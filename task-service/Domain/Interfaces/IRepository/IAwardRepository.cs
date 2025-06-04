using task_service.Domain.Entities;

namespace task_service.Domain.Interfaces.IRepository
{
    public interface IAwardRepository
    {
        Task<ICollection<Award>> GetAllAsync();
        Task<Award> GetAsync(Guid id);
    }
}
