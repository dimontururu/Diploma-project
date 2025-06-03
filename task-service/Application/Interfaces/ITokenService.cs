using task_service.Application.DTOs;
using task_service.Domain.Entities;

namespace task_service.Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(UserDTO userDTO);
    }
}
