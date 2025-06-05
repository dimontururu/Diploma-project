using TaskSlayerfrontendTGBot.ApiClient;

namespace TaskSlayerfrontendTGBot.Interface.IServices
{
    public interface ITokenService
    {
        Task<string> Refresh(UserDTO userDTO);
    }
}
