
using task_service.DTO;
using task_service.Models;

namespace task_service.UserService
{
    public class UserService:IUserService
    {
        private readonly ToDoListContext _DB;
        
        public UserService(ToDoListContext DB) 
        {
            _DB = DB;
        }

        public async Task CreateUser(NewUserDTO userDTO)
        {
            if (!DtoValidator.HasEmptyValues(userDTO))
            {
                var user = CreateNewUser(userDTO);
                var idClients = CreateIdClient(user, userDTO);
                await SaveUser(user);
                await SaveIdClient(idClients);
            }
            else
            { 
                throw new NullReferenceException("При создание пользователя поступили пустые данные");
            }    
        }

        public async Task<UserDTO> GetUserDTO(int id)
        {
            return null;
        }

        private User CreateNewUser(NewUserDTO userDTO)
        {
            var user = new User();
            user.Name = userDTO.Name;
            return user;
        }

        private IdClient CreateIdClient(User user,NewUserDTO userDto)
        {
            var idClient = new IdClient();
            idClient.IdClient1 = userDto.Id;
            idClient.IdUser = user.Id;
            
            bool idFound = false;
            foreach(var item in _DB.ClientTypes)
                if(item.Type == userDto.type_id)
                {
                    idClient.IdClientType = item.Id;
                    idFound = true;
                    break;
                }

            if (!idFound)
                throw new Exception("idClient не найден");

            return idClient;
        }

        private async Task SaveUser(User user)
        {
            await _DB.Users.AddAsync(user);
            await _DB.SaveChangesAsync();
        }

        private async Task SaveIdClient(IdClient idClient)
        {
            await _DB.IdClients.AddAsync(idClient);
            await _DB.SaveChangesAsync();
        }
    }
}
