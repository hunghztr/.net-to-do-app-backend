using Microsoft.AspNetCore.Mvc;
using ToDoList.Data;

namespace ToDoList.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class ToDoTaskController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public ToDoTaskController(ApplicationDBContext context)
        {
            _context = context;
        }
        

    }
}
