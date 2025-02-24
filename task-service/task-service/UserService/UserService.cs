
using task_service.Model;

namespace task_service.UserService
{
    public class UserService:IUserService
    {
        public UserService() { }

        public async Task CreateUser(NewUserDTO userDTO)
        {
            if (CheckForNull(userDTO))
            {
                var user = FromNewUserDTOToUser(userDTO);
                await SaveUser(user);
            }
            else
            { }    //как отправить обратно ошибку???
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

        private User FromNewUserDTOToUser(NewUserDTO userDTO)
        {
            var user = new User();
            user.Name = userDTO.Name;
            return user;
        }

        private async Task SaveUser(User user)
        {
            var db = new ToDoListContext();
            await db.Users.AddAsync(user);
            await db.SaveChangesAsync();
        }
    }
}
