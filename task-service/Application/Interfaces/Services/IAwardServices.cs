using task_service.Application.DTOs.AwardDTO;
using task_service.Domain.Entities;

namespace task_service.Application.Interfaces.Services
{
    public interface IAwardServices
    {
        Task<ICollection<ReturnAwardDTO>> GetAwards();
        Task<Award> GetAward(Guid id);
    }
}
