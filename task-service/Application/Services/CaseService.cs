using task_service.Application.DTOs.CaseDTO;
using task_service.Application.Interfaces.Services;
using task_service.Application.Validators;
using task_service.Domain.Entities;
using task_service.Domain.Interfaces.IRepository;

namespace task_service.Application.Services
{
    public class CaseService : ICaseService
    {
        private readonly ICaseRepository _caseRepository;
        public CaseService(ICaseRepository caseRepository) 
        {
            _caseRepository = caseRepository;
        }

        public async Task CreateCase(NewCaseDTO caseDTO, ToDoList toDoList)
        {
            if (DtoValidator.HasEmptyValues(caseDTO))
                throw new ArgumentException("дело поступило пустым");

            Case @case = CreateCase(caseDTO);

            @case.id_to_do_list = toDoList.Id;
            @case.id_to_do_listNavigation = toDoList;

            await _caseRepository.AddAsync(@case);
        }

        public async Task DeleteCase(Guid id)
        {
            await _caseRepository.DeleteAsync(id);
        }

        public async Task<ICollection<ReturnCaseDTO>> GetCases(ToDoList toDoList)
        {
            ICollection<ReturnCaseDTO> returnCaseDTOs = new List<ReturnCaseDTO>();

            foreach (var @case in toDoList.cases)
            {
                returnCaseDTOs.Add(new ReturnCaseDTO { 
                    Id = @case.Id,
                    DateEnd=@case.DateEnd,
                    Name=@case.Name,
                    Status=@case.Status,
                    DateOfCreation=@case.DateOfCreation,
                });

            }
            return returnCaseDTOs;
        }

        public async Task PutCase(PutCaseDTO PutCaseDTO)
        {
            Case @case = await _caseRepository.GetAsync(PutCaseDTO.id);

            @case.Name = PutCaseDTO.Name;
            @case.Status  = PutCaseDTO.Status;
            @case.DateEnd = PutCaseDTO.DateEnd;

            await _caseRepository.PutAsync(@case);
        }

        private Case CreateCase(NewCaseDTO newCaseDTO)
        {
            return new Case
            {
                Name = newCaseDTO.Name,
                Status = false,
                DateOfCreation = newCaseDTO.DateOfCreation,
                DateEnd = newCaseDTO.DateEnd,
            };
        }
    }
}
