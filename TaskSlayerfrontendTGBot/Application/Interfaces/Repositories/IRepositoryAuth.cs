using Domain.DTOs.User;

namespace Application.Interfaces.RepositoryApi
{
    public interface IRepositoryAuth
    {
        Task<string> Authorization(UserDTO user);
    }
}
