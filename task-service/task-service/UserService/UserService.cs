
using task_service.DTO;
using task_service.Models;

namespace task_service.UserService
{
    public class UserService:IUserService
    {
        public UserService() { }

        public async Task CreateUser(NewUserDTO userDTO)
        {
            if (CheckForNull(userDTO))
            {
                var user = CreateNewUser(userDTO);
                await SaveUser(user);
                var idClients = CreateIdClient(user, userDTO);
                await SaveIdClient(idClients);
            }
            else
            { 
                throw new NullReferenceException();
            }    
        }

        public async Task<UserDTO> GetUserDTO(int id)
        {
            return null;
        }

        private bool CheckForNull(NewUserDTO userDTO)
        {
            if (userDTO == null)
                return false;

            if(userDTO.Name== null)
                return false;
            
            return true;
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

            using(var db = new ToDoListContext())
            {
                foreach(var item in db.ClientTypes)
                    if(item.Type == userDto.type_id)
                    {
                        idClient.IdClientType = item.Id;
                        break;
                    }
            }

            return idClient;
        }

        private async Task SaveUser(User user)
        {
            var db = new ToDoListContext();
            await db.Users.AddAsync(user);
            await db.SaveChangesAsync();
        }

        private async Task SaveIdClient(IdClient idClient)
        {
            var db = new ToDoListContext();
            await db.IdClients.AddAsync(idClient);
            await db.SaveChangesAsync();
        }
    }
}
