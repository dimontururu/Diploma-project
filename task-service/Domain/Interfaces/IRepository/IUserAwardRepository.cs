using task_service.Domain.Entities;

namespace task_service.Domain.Interfaces.IRepository
{
    public interface IUserAwardRepository
    {
        Task<ICollection<Award>> getAwardsAsync(User user);
        Task PutAsync(User user, Award award);
    }
}
