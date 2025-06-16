using Application.Interfaces.ApiClients;
using Application.Interfaces.RepositoryApi;
using Domain.DTOs.ToDoList;

namespace Infrastructure.RepositoryApi
{
    internal class RepositoryToDoList : IRepositoryToDoList
    {
        public async Task CreateToDoList(NewToDoListDTO doListDTO, IUserScopedApiClient userScopedApiClient)
        {
            await userScopedApiClient.ExecuteAsUserAsync(async action =>
            {
                await action.CreateTodolistAsync(doListDTO);
            });
        }

        public async  Task DeleteToDoList(Guid idToDoList, IUserScopedApiClient userScopedApiClient)
        {
            await userScopedApiClient.ExecuteAsUserAsync(async action =>
            {
                await action.DeleteToDoListAsync(idToDoList);
            });
        }

        public async Task<ICollection<ReturnToDoListsDTO>> GetToDoLists(IUserScopedApiClient userScopedApiClient)
        {
            return await userScopedApiClient.ExecuteAsUserAsync(async action =>
            {
                return await action.GetToDoListsAsync();
            });
        }

        public async Task PutToDoList(PutToDoListDTO toDoListDTO, IUserScopedApiClient userScopedApiClient)
        {
            await userScopedApiClient.ExecuteAsUserAsync(async action =>
            {
                await action.PutToDoListAsync(toDoListDTO);
            });
        }
    }
}
