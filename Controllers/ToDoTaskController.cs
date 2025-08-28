using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Data;
using ToDoList.Interfaces;
using ToDoList.Models;
using ToDoList.Utils;

namespace ToDoList.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class ToDoTaskController : ControllerBase
    {
        private readonly IToDoTaskRepository _toDoTaskRepository;
        private readonly UserManager<User> _userManager;
        public ToDoTaskController(IToDoTaskRepository toDoTaskRepository,
            UserManager<User> userManager)
        {
            _toDoTaskRepository = toDoTaskRepository;
            _userManager = userManager;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            return Ok(await _toDoTaskRepository.GetAll(query));
        }
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var task = await _toDoTaskRepository.GetById(id);
            if (task == null) return BadRequest("Task not found");
            return Ok(task);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] ToDoTask task)
        {
            var username = User.FindFirst("name")?.Value;
            var user = await _userManager.FindByNameAsync(username);
            if(user == null) return BadRequest("User not found");
            task.UserId = user.Id;
            var createdTask = await _toDoTaskRepository.Create(task);
            return CreatedAtAction(nameof(GetById), new { id = createdTask.Id }, createdTask);
        }
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] ToDoTask task, [FromRoute] int id)
        {
            var updatedTask = await _toDoTaskRepository.Update(task, id);
            if (updatedTask == null) return BadRequest("Task not found");
            return Ok(updatedTask);
        }
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var deletedTask = await _toDoTaskRepository.Delete(id);
            if (deletedTask == null) return BadRequest("Task not found");
            return Ok(deletedTask);
        }

    }
}
