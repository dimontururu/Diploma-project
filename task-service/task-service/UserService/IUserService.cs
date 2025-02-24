namespace task_service.UserService
{
    public interface IUserService
    {
        Task CreateUser(NewUserDTO user);
        Task<UserDTO> GetUserDTO(int id);//разобраться как тг хранит пользователей
    }
}
