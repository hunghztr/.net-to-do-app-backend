using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace ToDoList.Models
{
    public class User : IdentityUser
    {
        
        public string RefreshToken { get; set; } = string.Empty;
        [JsonIgnore]
        public List<ToDoTask> ToDoTasks { get; set; } = new List<ToDoTask>();
        [JsonIgnore]
        public List<UploadedFile> Files { get; set; } = new List<UploadedFile>();
    }
}
