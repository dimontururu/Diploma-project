using task_service.Domain.Interfaces.IRepository;
using task_service.Domain.Entities;

namespace task_service.Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ToDoListContext _DB;

        public UserRepository(ToDoListContext DB) 
        {
            _DB = DB;
        }

        public async Task AddAsync(User user)
        {
            await _DB.Users.AddAsync(user);
            await _DB.SaveChangesAsync();
        }

        public async Task<User> GetAsync(Guid id)
        {
            return await _DB.Users.FindAsync(id);
        }
    }
}
