using task_service.Application.DTOs.ToDoListDTO;
using task_service.Application.Interfaces.Services;
using task_service.Application.Validators;
using task_service.Domain.Entities;
using task_service.Domain.Interfaces.IRepository;

namespace task_service.Application.Services
{
    internal class ToDoListService : IToDoListService
    {
        private readonly IToDoListRepository _toDoListRepository;
        private readonly ICaseRepository _caseRepository;
        public ToDoListService(IToDoListRepository toDoListRepository,ICaseRepository caseRepository) 
        {
            _toDoListRepository = toDoListRepository;
            _caseRepository = caseRepository;
        }

        public async Task<ToDoList> CreateToDoList(NewToDoListDTO toDoListDTO, User user)
        {
            if (DtoValidator.HasEmptyValues(toDoListDTO))
                throw new ArgumentException("Списка дел поступили пустые данне");

            ToDoList toDoList = CreateNewToDoList(toDoListDTO);

            toDoList.IdUser = user.Id;
            toDoList.IdUserNavigation = user;

            await _toDoListRepository.AddAsync(toDoList);

            return toDoList;
        }

        public async Task DeleteToDoList(Guid id)
        {
            ToDoList toDoList = await _toDoListRepository.GetAsync(id);
            ICollection<Case> cases = toDoList.cases.ToList();
            foreach (Case c in cases)
                await _caseRepository.DeleteAsync(c.Id);

            await _toDoListRepository.DeleteAsync(id);
        }

        public async Task<ICollection<ReturnToDoListsDTO>> GetToDoLists(User user)
        {
            ICollection<ReturnToDoListsDTO> returnToDoListsDTO = new List<ReturnToDoListsDTO>();

            foreach (var toDoList in user.ToDoLists)
            {
                returnToDoListsDTO.Add(new ReturnToDoListsDTO{
                    Id = toDoList.Id.ToString(),
                    Name = toDoList.Name
                });
                    
            }
            return returnToDoListsDTO;
        }

        public async Task PutToDoList(PutToDoListDTO putToDoListDTO)
        {
            ToDoList toDoList = await _toDoListRepository.GetAsync(putToDoListDTO.Id);
            toDoList.Name = putToDoListDTO.Name;
            await _toDoListRepository.PutAsync(toDoList);
        }

        public async Task<ToDoList> GetToDoList(Guid id)
        {
            return await _toDoListRepository.GetAsync(id);
        }

        private ToDoList CreateNewToDoList(NewToDoListDTO toDoListDTO)
        {
            return new ToDoList { Name = toDoListDTO.Name};
        }
    }
}
