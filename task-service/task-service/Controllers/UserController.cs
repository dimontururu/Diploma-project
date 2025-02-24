using Microsoft.AspNetCore.Mvc;
using task_service.Model;
using task_service.UserService;

namespace task_service.Controllers
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
