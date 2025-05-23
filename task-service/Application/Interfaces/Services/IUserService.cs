using task_service.Application.DTOs;

namespace task_service.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task CreateUser(NewUserDTO user);
        Task<UserDTO> GetUserDTO(int id);//разобраться как тг хранит пользователей
    }
}
