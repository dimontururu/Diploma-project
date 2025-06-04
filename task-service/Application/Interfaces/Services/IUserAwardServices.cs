using task_service.Application.DTOs.AwardDTO;
using task_service.Domain.Entities;

namespace task_service.Application.Interfaces.Services
{
    public interface IUserAwardServices
    {
        Task PutUserAward(User user, Award award);
        Task<ICollection<ReturnAwardDTO>> GetUserAward(User user);
    }
}
