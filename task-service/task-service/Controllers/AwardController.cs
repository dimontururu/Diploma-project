﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using task_service.Application.DTOs.AwardDTO;
using task_service.Application.Interfaces.Services;

namespace task_service.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AwardController:ControllerBase
    {
        private readonly IAwardServices _awardServices;
        public AwardController(IAwardServices awardServices) 
        {
            _awardServices = awardServices;
        }

        [Authorize]
        [HttpGet("GetAwards")]
        [ProducesResponseType(typeof(ICollection<ReturnAwardDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAwards()
        {
            return Ok(await _awardServices.GetAwards());
        }
    }
}
