using task_service.Application.DTOs.AwardDTO;
using task_service.Application.Interfaces.Services;
using task_service.Domain.Entities;
using task_service.Domain.Interfaces.IRepository;

namespace task_service.Application.Services
{
    public class UserAwardServices : IUserAwardServices
    {
        private readonly IUserAwardRepository _userRepository;
        public UserAwardServices(IUserAwardRepository userRepository) 
        {
            _userRepository = userRepository;
        }
        public async Task<ICollection<ReturnAwardDTO>> GetUserAward(User user)
        {
            ICollection<ReturnAwardDTO> returnAwardDTOs = new List<ReturnAwardDTO>();

            foreach (Award award in await _userRepository.getAwardsAsync(user))
            {
                returnAwardDTOs.Add(new ReturnAwardDTO
                {
                    id = award.Id,
                    name = award.Name
                });
            }

            return returnAwardDTOs;
        }

        public async Task PutUserAward(User user, Award award)
        {
            await _userRepository.PutAsync(user, award);
        }
    }
}
