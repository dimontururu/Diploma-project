using task_service.Domain.Entities;

namespace task_service.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
    }
}
