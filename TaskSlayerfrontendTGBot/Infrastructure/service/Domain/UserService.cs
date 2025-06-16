using Application.Interfaces.RepositoryApi;
using Application.Services.Domain;
using Domain.DTOs.User;
using Telegram.Bot.Types;

namespace Infrastructure.service.Command
{
    internal class UserService:IUserService
    {
        private readonly IRepositoryUser _repositoryUser;
        public UserService(IRepositoryUser repositoryUser)
        {
            _repositoryUser = repositoryUser;
        }
        public async Task CreateUser(User userTelegram)
        {
            var userDTO = new UserDTO
            {
                Id = userTelegram.Id.ToString(),
                Name = userTelegram.Username,
                Type_id = "Telegram"
            };

            await _repositoryUser.CreateUser(userDTO);
        }
    }
}
