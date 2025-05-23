using task_service.Application.Interfaces.Services;
using task_service.Domain.Interfaces.IRepository;
using task_service.Application.DTOs;
using task_service.Application.Validators;
using task_service.Domain.Entities;

namespace task_service.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IIdClientRepository _idClientRepository;
        private readonly IClientTypeRepository _clientTypeRepository;

        public UserService(
            IUserRepository userRepository,
            IIdClientRepository idClientRepository,
            IClientTypeRepository clientTypeRepository)
        {
            _userRepository = userRepository;
            _idClientRepository = idClientRepository;
            _clientTypeRepository = clientTypeRepository;
        }

        public async Task CreateUser(NewUserDTO userDTO)
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
        }

        public async Task<UserDTO> GetUserDTO(int id)
        {
            //var user = await _userRepository.GetByIdAsync(id)
            //    ?? throw new KeyNotFoundException("Пользователь не найден");

            //return new UserDTO
            //{
            //    Id = user.Id,
            //    Name = user.Name
            //};
            return null;
        }

        private User CreateNewUser(NewUserDTO userDTO)
        {
            return new User { Name = userDTO.Name };
        }
    }
}
