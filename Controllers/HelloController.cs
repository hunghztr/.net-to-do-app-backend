using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Utils;

namespace ToDoList.Controllers
{
    [ApiController]
    [Route("api/hello")]
    public class HelloController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            throw new ValidException("This is a custom exception for demonstration purposes.");
            return Ok("Hello, World!");
        }
    }
}
