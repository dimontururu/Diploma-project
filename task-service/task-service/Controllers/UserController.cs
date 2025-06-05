using task_service.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using task_service.Application.Interfaces.Services;
using task_service.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

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

        [HttpPost("CreateUser")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [Produces("text/plain")]
        public async Task<ActionResult> CreateUser(UserDTO userDTO)
        {
            var user = await _userService.CreateUser(userDTO);

            var token = _tokenService.GenerateToken(userDTO);

            return Content(token, "text/plain");
        }

        [Authorize]
        [HttpGet("GetUser")]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetUser()
        {
            var userDTO = new UserDTO
            {
                Id = User.FindFirstValue("client_id"),
                type_id = User.FindFirstValue("client_type"),

            };

            return Ok(userDTO);
        }
    }
}
