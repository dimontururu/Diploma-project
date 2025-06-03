using task_service.Application.Interfaces.Services;
using task_service.Domain.Interfaces.IRepository;
using task_service.Application.DTOs;
using task_service.Application.Validators;
using task_service.Domain.Entities;
using task_service.Application.Interfaces;

namespace task_service.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IIdClientRepository _idClientRepository;
        private readonly IClientTypeRepository _clientTypeRepository;
        private readonly ITokenService _tokenService;

        public UserService(
            IUserRepository userRepository,
            IIdClientRepository idClientRepository,
            IClientTypeRepository clientTypeRepository,
            ITokenService tokenService)
        {
            _userRepository = userRepository;
            _idClientRepository = idClientRepository;
            _clientTypeRepository = clientTypeRepository;
            _tokenService = tokenService;
        }

        public async Task<User> CreateUser(UserDTO userDTO)
        {
            if (DtoValidator.HasEmptyValues(userDTO))
                throw new ArgumentException("При создании пользователя поступили пустые данные");

            var user = CreateNewUser(userDTO);

            var clientType = await _clientTypeRepository.GetByTypeAsync(userDTO.type_id)
                ?? throw new KeyNotFoundException("Тип клиента не найден");

            await _userRepository.AddAsync(user);

            var idClient = new IdClient
            {
                IdClient1 = userDTO.Id,
                IdUser = user.Id,
                IdClientType = clientType.Id
            };

            await _idClientRepository.AddAsync(idClient);

            return user;
        }

        public async Task<User> GetUser(UserDTO userDTO)
        {
            if (DtoValidator.HasEmptyValues(userDTO))
                throw new ArgumentException("При авторизации пользователя поступили пустые данные");

            ClientType clientType = await _clientTypeRepository.GetByTypeAsync(userDTO.type_id)
                ?? throw new KeyNotFoundException("Тип клиента не найден");

            IdClient idClient = await _idClientRepository.GetAsync(clientType.Id,userDTO.Id)
                ?? throw new KeyNotFoundException("id типа клиента не найден");

            User user = await _userRepository.GetAsync(idClient.IdUser)
                ?? throw new KeyNotFoundException("Пользователь не найден");

            return user;
        }

        private User CreateNewUser(UserDTO userDTO)
        {
            return new User { Name = userDTO.Name };
        }
    }
}
