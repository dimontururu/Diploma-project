using task_service.Application.DTOs.AwardDTO;

namespace task_service.Application.Interfaces.Services
{
    public interface IAwardServices
    {
        Task<ICollection<ReturnAwardDTO>> GetAwards();
    }
}
