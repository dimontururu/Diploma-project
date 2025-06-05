using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using task_service.Application.DTOs.AwardDTO;
using task_service.Application.DTOs.CaseDTO;
using task_service.Application.Interfaces.Services;
using task_service.Domain.Entities;

namespace task_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaseController:ControllerBase
    {
        private readonly ICaseService _caseService;
        private readonly IToDoListService _toDoListService;
        public CaseController(ICaseService caseService,IToDoListService toDoListService) 
        { 
            _caseService = caseService;
            _toDoListService = toDoListService;
        }

        [Authorize]
        [HttpPost("CreateCase")]
        public async Task<IActionResult> CreateCase(NewCaseDTO newCaseDTO, Guid idToDoList)
        {
            ToDoList toDoList = await _toDoListService.GetToDoList(idToDoList);

            await _caseService.CreateCase(newCaseDTO, toDoList);

            return Ok();
        }

        [Authorize]
        [HttpGet("GetCases")]
        [ProducesResponseType(typeof(ICollection<ReturnCaseDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCases(Guid idToDoList)
        {
            ToDoList toDoList = await _toDoListService.GetToDoList(idToDoList);

            ICollection<ReturnCaseDTO> returnCaseDTO = await _caseService.GetCases(toDoList);

            return Ok(returnCaseDTO);
        }

        [Authorize]
        [HttpDelete("DeleteCase")]
        public async Task<IActionResult> DeleteCase(Guid idCase)
        {
            await _caseService.DeleteCase(idCase);

            return Ok();
        }

        [Authorize]
        [HttpPut("PutCase")]
        public async Task<IActionResult> PutCase(PutCaseDTO putCase)
        {
            await _caseService.PutCase(putCase);

            return Ok();
        }
    }
}
