using System.ComponentModel.DataAnnotations;
using ToDoList.Utils.Attribute;

namespace ToDoList.Models
{
    public class ToDoTask
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string description { get; set; }
        public bool Status { get; set; }
        [ValidDueDate]
        public DateTime DueDate { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
