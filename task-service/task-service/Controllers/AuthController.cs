using Microsoft.AspNetCore.Mvc;
using task_service.Application.DTOs;
using task_service.Application.Interfaces;
using task_service.Application.Interfaces.Services;
using task_service.Domain.Entities;

namespace ttask_service.Presentation.Controllers
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
            await _userService.GetUser(authorizationDTO);
            var token = _tokenService.GenerateToken(authorizationDTO);
            return Ok(token);
        }
    }
}
