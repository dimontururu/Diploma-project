using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using task_service.Application.DTOs;
using task_service.Application.DTOs.AwardDTO;
using task_service.Application.Interfaces.Services;
using task_service.Domain.Entities;

namespace task_service.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAwardController:ControllerBase
    {
        private readonly IUserAwardServices _userAwardServices;
        private readonly IUserService _userService;
        private readonly IAwardServices _awardServices;
        public  UserAwardController(IUserAwardServices userAwardServices, IUserService userService,IAwardServices awardServices) 
        {
            _userAwardServices = userAwardServices;
            _userService = userService;
            _awardServices=awardServices;
        }

        [Authorize]
        [HttpPost("PostUserAward")]
        public async Task<IActionResult>PostUserAward(Guid idAward)
        {
            await _userAwardServices.PutUserAward(await UserFromToken(), await _awardServices.GetAward(idAward));
            return Ok();
        }

        [Authorize]
        [HttpGet("GetUserAward")]
        [ProducesResponseType(typeof(ICollection<ReturnAwardDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserAward()
        {
            return Ok(await _userAwardServices.GetUserAward(await UserFromToken()));
        }

        private async Task<User> UserFromToken()
        {
            var userDTO = new UserDTO
            {
                Name = "Костыль",
                Id = User.FindFirstValue("client_id"),
                type_id = User.FindFirstValue("client_type"),

            };

            return await _userService.GetUser(userDTO);
        }
    }
}
