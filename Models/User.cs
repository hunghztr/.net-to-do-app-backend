using Microsoft.AspNetCore.Identity;

namespace ToDoList.Models
{
    public class User : IdentityUser
    {
        public List<ToDoTask> ToDoTasks { get; set; } = new List<ToDoTask>();
    }
}
