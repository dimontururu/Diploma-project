using Application.Bot;
using Application.Interfaces.RepositoryApi;
using Application.Services.Domain;
using Domain.DTOs.Case;
using Telegram.Bot.Types;

namespace Infrastructure.service.Domain
{
    internal class CaseService : ICaseService
    {
        private readonly IRepositoryCase _repositoryCase;
        private readonly IUserApiResolver _userApiResolver;
        public CaseService(IRepositoryCase repositoryCase,IUserApiResolver userApiResolver)
        {
            _repositoryCase = repositoryCase;
            _userApiResolver = userApiResolver;
        }

        public async Task CreateCase(Guid idToDoList,NewCaseDTO caseDTO, User user)
        {
            var api = _userApiResolver.Resolve(user);
            await _repositoryCase.CreateCase(idToDoList, caseDTO,api);
        }

        public async Task<ICollection<ReturnCaseDTO>> GetCases(Guid idToDoList,User user)
        {
            var api = _userApiResolver.Resolve(user);
            return await _repositoryCase.GetCases(idToDoList,api);
        }

        public async Task DeleteCase(Guid idCase, User user)
        {
            var api = _userApiResolver.Resolve(user);
            await _repositoryCase.DeleteCases(idCase, api);
        }

        public async Task PutCase(PutCaseDTO caseDTO, User user)
        {
            var api = _userApiResolver.Resolve(user);
            await _repositoryCase.PutCase(caseDTO,api);
        }
    }
}
