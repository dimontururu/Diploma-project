using Application.Interfaces.ApiClients;
using Application.Interfaces.RepositoryApi;
using Domain.DTOs.Case;

namespace Infrastructure.RepositoryApi
{
    internal class RepositoryCase : IRepositoryCase
    {
        public async Task CreateCase(Guid idToDoList, NewCaseDTO caseDTO, IUserScopedApiClient userScopedApiClient)
        {
            await userScopedApiClient.ExecuteAsUserAsync(async action =>
            {
                await action.CreateCaseAsync(idToDoList, caseDTO);
            });
        }

        public async Task DeleteCases(Guid idCase, IUserScopedApiClient userScopedApiClient)
        {
            await userScopedApiClient.ExecuteAsUserAsync(async action =>
            {
                await action.DeleteCaseAsync(idCase);
            });
        }

        public async Task<ICollection<ReturnCaseDTO>> GetCases(Guid idToDoList, IUserScopedApiClient userScopedApiClient)
        {
            return await userScopedApiClient.ExecuteAsUserAsync(async action =>
            {
                return await action.GetCasesAsync(idToDoList);
            });
        }

        public async Task PutCase(PutCaseDTO caseDTO, IUserScopedApiClient userScopedApiClient)
        {
            await userScopedApiClient.ExecuteAsUserAsync(async action =>
            {
                await action.PutCaseAsync(caseDTO);
            });
        }
    }
}
