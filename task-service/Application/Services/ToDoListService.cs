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
        private IToDoListRepository _toDoListRepository;
        public ToDoListService(IToDoListRepository toDoListRepository) 
        {
            _toDoListRepository = toDoListRepository;
        }

        public async Task<ToDoList> CreateToDoList(NewToDoListDTO toDoListDTO, User user)
        {
            if (DtoValidator.HasEmptyValues(toDoListDTO))
                throw new ArgumentException("Списка дел поступили пустые данне");

            ToDoList toDoList = CreateNewToDoList(toDoListDTO);

            toDoList.IdUser = user.Id;

            //user.ToDoLists.Add(toDoList);

            await _toDoListRepository.AddAsync(toDoList);

            return toDoList;
        }

        //public async Task<ReturnToDoListsDTO> GetToDoList(Guid Id)
        //{
        //    ToDoList toDoList = await _toDoListRepository.GetAsync(Id)
        //         ?? throw new KeyNotFoundException("Тип клиента не найден");

        //    return toDoList;
        //}

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

        private ToDoList CreateNewToDoList(NewToDoListDTO toDoListDTO)
        {
            return new ToDoList { Name = toDoListDTO.Name};
        }
    }
}
