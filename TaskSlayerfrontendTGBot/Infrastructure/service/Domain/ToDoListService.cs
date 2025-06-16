using Application.Bot;
using Application.Interfaces.RepositoryApi;
using Application.Services.Domain;
using Domain.DTOs.ToDoList;
using Telegram.Bot.Types;

namespace Infrastructure.service.Domain
{
    internal class ToDoListService: IToDoListService
    {
        private readonly IRepositoryToDoList _repositoryToDoList;
        private readonly IUserApiResolver _userApiResolver;

        public ToDoListService(IRepositoryToDoList repositoryToDoList,IUserApiResolver userApiResolver) 
        {
            _repositoryToDoList = repositoryToDoList;
            _userApiResolver = userApiResolver;
        }

        public async Task CreateToDoList(string nameToDoList,User user)
        {
            var api = _userApiResolver.Resolve(user);

            var toDoList = new NewToDoListDTO
            {
                Name = nameToDoList
            };

            await _repositoryToDoList.CreateToDoList(toDoList,api);
        }

        public async Task<ICollection<ReturnToDoListsDTO>> GetToDoLists(User user)
        {
            var api = _userApiResolver.Resolve(user);
            return await _repositoryToDoList.GetToDoLists(api);
        }

        public async Task DeleteToDoList(Guid IdToDoList, User user)
        {
            var api = _userApiResolver.Resolve(user);
            await _repositoryToDoList.DeleteToDoList(IdToDoList, api);
        }

        public async Task EditToDoList(PutToDoListDTO putToDoListDTO,User user)
        {
            var api = _userApiResolver.Resolve(user);
            await _repositoryToDoList.PutToDoList(putToDoListDTO,api);
        }
    }
}
