using Microsoft.AspNetCore.Mvc;

namespace task_service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToDoListController : ControllerBase
    {
        [HttpDelete]
        public void Delete() 
        { }
    }
}
