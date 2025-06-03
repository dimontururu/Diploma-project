using Microsoft.AspNetCore.Mvc;
using task_service.Application.DTOs;
using task_service.Application.Interfaces;
using task_service.Application.Interfaces.Services;
using task_service.Domain.Entities;

namespace task_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController: ControllerBase
    {
        private ITokenService _tokenService;
        private IUserService _userService;

        public AuthController(ITokenService tokenService,IUserService userService)
        { 
            _tokenService = tokenService;
            _userService = userService;
        }

        [HttpPost("authorization")]
        public async Task<IActionResult> authorization(UserDTO authorizationDTO ) 
        {
            User user = await _userService.GetUser(authorizationDTO);
            var token = _tokenService.GenerateToken(user);
            return Ok(token);
        }
    }
}
