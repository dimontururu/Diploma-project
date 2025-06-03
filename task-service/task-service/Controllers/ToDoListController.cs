using Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using task_service.Application.DTOs;
using task_service.Application.Interfaces.Services;
using task_service.Domain.Entities;

namespace task_service.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoListController : ControllerBase
    {
        private readonly IToDoListService _toDoListService;
        private readonly IUserService _userService;
        public ToDoListController(IToDoListService toDoListService, IUserService userService) 
        {
            _toDoListService = toDoListService;
            _userService = userService;
        }

        [Authorize]
        [HttpPost("CreateTodolist")]
        public async Task<IActionResult> CreateTo_Do_List(NewToDoListDTO toDoListDTO)
        {
            var userDTO = new UserDTO
            {
                Name = "Костыль",
                Id = User.FindFirstValue("client_id"),
                type_id = User.FindFirstValue("client_type"),

            };

            User user = await _userService.GetUser(userDTO);

            await _toDoListService.CreateToDoList(toDoListDTO, user);

            return Ok();
        }

        [Authorize]
        [HttpGet("GetToDoLists")]
        public async Task<IActionResult> GetToDoLists()
        {
            var userDTO = new UserDTO
            {
                Name = "Костыль",
                Id = User.FindFirstValue("client_id"),
                type_id = User.FindFirstValue("client_type"),

            };

            User user = await _userService.GetUser(userDTO);
            ICollection<ReturnToDoListsDTO> toDoLists = await _toDoListService.GetToDoLists(user);
            return Ok(toDoLists);
        }


    }
}
