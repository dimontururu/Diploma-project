using Application.Interfaces.ApiClients;
using Domain.DTOs.ToDoList;

namespace Application.Interfaces.RepositoryApi
{
    public interface IRepositoryToDoList
    {
        Task CreateToDoList(NewToDoListDTO doListDTO, IUserScopedApiClient userScopedApiClient);
        Task<ICollection<ReturnToDoListsDTO>> GetToDoLists(IUserScopedApiClient userScopedApiClient);
        Task DeleteToDoList(Guid idToDoList, IUserScopedApiClient userScopedApiClient);
        Task PutToDoList(PutToDoListDTO toDoListDTO, IUserScopedApiClient userScopedApiClient);
    }
}
