using task_service.Application.DTOs;
using task_service.Domain.Entities;

namespace task_service.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<User> CreateUser(NewUserDTO user);
        Task<UserDTO> GetUserDTO(int id);//разобраться как тг хранит пользователей
    }
}
