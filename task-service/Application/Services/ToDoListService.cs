using Application.DTOs;
using task_service.Application.DTOs;
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

        public async Task DeleteToDoList(Guid id, User user)
        {
            CheckIfTheUserHasAToDoList(id, user);

            ToDoList toDoList = await _toDoListRepository.GetAsync(id);
            foreach (Case c in toDoList.cases)
                _toDoListRepository.DeleteAsync(c.Id);

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

        public async Task PutToDoList(PutToDoListDTO putToDoListDTO, User user)
        {
            CheckIfTheUserHasAToDoList(putToDoListDTO.Id, user);

            ToDoList toDoList = await _toDoListRepository.GetAsync(putToDoListDTO.Id);
            toDoList.Name = putToDoListDTO.Name;
            await _toDoListRepository.PutAsync(toDoList);
        }

        private ToDoList CreateNewToDoList(NewToDoListDTO toDoListDTO)
        {
            return new ToDoList { Name = toDoListDTO.Name};
        }

        private void CheckIfTheUserHasAToDoList(Guid id, User user)
        {
            //есть ли у пользователя этот список задач
            bool found = false;
            foreach (var t in user.ToDoLists)
            {
                if (t.Id == id)
                {
                    found = true;
                    break;
                }
            }

            if (!found)
                throw new KeyNotFoundException("У пользователя не найде такая задача");
        }
    }
}
