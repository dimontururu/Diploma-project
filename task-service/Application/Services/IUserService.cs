
using task_service.Application.DTOs;

namespace task_service.Application.Services
{
    public interface IUserService
    {
        Task CreateUser(NewUserDTO user);
        Task<UserDTO> GetUserDTO(int id);//разобраться как тг хранит пользователей
    }
}
