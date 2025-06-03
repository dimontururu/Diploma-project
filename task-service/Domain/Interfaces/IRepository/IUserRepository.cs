using task_service.Domain.Entities;

namespace task_service.Domain.Interfaces.IRepository
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
        Task<User> GetAsync(Guid id);
    }
}
