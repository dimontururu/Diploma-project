using Application.Bot;
using Application.Interfaces.RepositoryApi;
using Application.Services.Domain;
using Domain.DTOs.Award;
using Telegram.Bot.Types;

namespace Infrastructure.service.Domain
{
    internal class AwardService:IAwardService
    {
        private readonly IRepositoryAward _repositoryAward;
        private readonly IRepositoryUserAward _userAward;
        private readonly IUserApiResolver _userApiResolver;

        public AwardService(IRepositoryAward repositoryAward, IRepositoryUserAward userAward,IUserApiResolver userApiResolver)
        {
            _repositoryAward = repositoryAward;
            _userAward = userAward;
            _userApiResolver = userApiResolver;
        }

        public async Task<ICollection<ReturnAwardDTO>> GetAwards(User user)
        {
            var api = _userApiResolver.Resolve(user);
            return await _repositoryAward.GetAwards(api);
        }

        public async Task SetUserAwarad(Guid idAward,User user)
        {
            var api = _userApiResolver.Resolve(user);
            await _userAward.PostUserAward(idAward,api);
        }
        
        public async Task<ICollection<ReturnAwardDTO>> GetUserAwarads(User user)
        {
            var api = _userApiResolver.Resolve(user);
            return await _userAward.GetUserAward(api);
        }
    }
}
