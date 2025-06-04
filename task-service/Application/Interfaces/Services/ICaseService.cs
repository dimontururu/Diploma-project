using task_service.Application.DTOs.CaseDTO;
using task_service.Domain.Entities;

namespace task_service.Application.Interfaces.Services
{
    public interface ICaseService
    {
        Task CreateCase(NewCaseDTO caseDTO, ToDoList toDoList);

        Task<ICollection<ReturnCaseDTO>> GetCases(ToDoList toDoList);

        Task DeleteCase(Guid id);
        Task PutCase(PutCaseDTO putCaseDTO);
    }
}
