using Domain.DTOs.Award;
using Telegram.Bot.Types;

namespace Application.Services.Domain
{
    public interface IAwardService
    {
        Task<ICollection<ReturnAwardDTO>> GetAwards(User user);
        Task SetUserAwarad(Guid idAward, User user);
        Task<ICollection<ReturnAwardDTO>> GetUserAwarads(User user);
    }
}
