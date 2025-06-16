using Application.Interfaces.ApiClients;
using Domain.DTOs.Case;

namespace Application.Interfaces.RepositoryApi
{
    public interface IRepositoryCase
    {
        Task CreateCase(Guid idToDoList, NewCaseDTO caseDTO, IUserScopedApiClient userScopedApiClient);
        Task<ICollection<ReturnCaseDTO>> GetCases(Guid idToDoList, IUserScopedApiClient userScopedApiClient);
        Task DeleteCases(Guid idCase, IUserScopedApiClient userScopedApiClient);
        Task PutCase(PutCaseDTO caseDTO, IUserScopedApiClient userScopedApiClient);
    }
}
