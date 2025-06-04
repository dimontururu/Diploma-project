using Microsoft.EntityFrameworkCore;
using task_service.Domain.Entities;
using task_service.Domain.Interfaces.IRepository;

namespace task_service.Infrastructure.Data.Repositories
{
    public class UserAwardRepository : IUserAwardRepository
    {
        private readonly ToDoListContext _DB;
        public UserAwardRepository(ToDoListContext DB) 
        {
            _DB = DB;
        }
        public async Task<ICollection<Award>> getAwardsAsync(User user)
        {
            return user.IdAwards;
        }

        public async Task PutAsync(User user, Award award)
        {
            user.IdAwards.Add(award);
            _DB.Entry(user).State = EntityState.Modified;
            await _DB.SaveChangesAsync();
        }
    }
}
