using Domain.DTOs.Case;
using Telegram.Bot.Types;

namespace Application.Services.Domain
{
    public interface ICaseService
    {
        Task<ICollection<ReturnCaseDTO>> GetCases(Guid idToDoList, User user);
        Task CreateCase(Guid idToDoList, NewCaseDTO caseDTO, User user);
        Task DeleteCase(Guid idCase, User user);
        Task PutCase(PutCaseDTO caseDTO, User user);
    }
}
