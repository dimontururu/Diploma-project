using task_service.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using task_service.Application.Interfaces.Services;
using task_service.Application.Interfaces;

namespace task_service.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public UserController(IUserService userService, ITokenService tokenService) 
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser(NewUserDTO userDTO)
        {
            var user = await _userService.CreateUser(userDTO);

            var token = _tokenService.GenerateToken(user);

            return Ok(token);
        }
    }
}
