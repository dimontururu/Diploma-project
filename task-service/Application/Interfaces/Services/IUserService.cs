using task_service.Application.DTOs;
using task_service.Domain.Entities;

namespace task_service.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<User> CreateUser(UserDTO user);
        Task<User> GetUser(UserDTO user);//разобраться как тг хранит пользователей
    }
}
