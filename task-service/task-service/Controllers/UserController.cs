using task_service.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using task_service.Application.Interfaces.Services;

namespace task_service.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        public UserController(IUserService userService) 
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser(NewUserDTO user)
        {
            await _userService.CreateUser(user);

            return Ok();
        }
    }
}
