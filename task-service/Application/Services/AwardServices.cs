using task_service.Application.DTOs.AwardDTO;
using task_service.Application.Interfaces.Services;
using task_service.Domain.Entities;
using task_service.Domain.Interfaces.IRepository;

namespace task_service.Application.Services
{
    public class AwardServices : IAwardServices
    {
        private readonly IAwardRepository _awardRepository;
        public AwardServices(IAwardRepository awardRepository) 
        { 
            _awardRepository = awardRepository;
        }

        public async Task<Award> GetAward(Guid id)
        {
            return await _awardRepository.GetAsync(id);
        }

        public async Task<ICollection<ReturnAwardDTO>> GetAwards()
        {
            ICollection<Award> Awards = await _awardRepository.GetAllAsync();

            ICollection<ReturnAwardDTO> returnAwardDTOs = new List<ReturnAwardDTO>();

            foreach (Award award in Awards)
            {
                returnAwardDTOs.Add(new ReturnAwardDTO
                {
                    name = award.Name,
                    id = award.Id,
                });
            }

            return returnAwardDTOs;
        }
    }
}
