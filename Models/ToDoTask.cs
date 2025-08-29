using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ToDoList.Utils.Attribute;

namespace ToDoList.Models
{
    public class ToDoTask
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Column(TypeName = "NVARCHAR(MAX)")]
        public string Description { get; set; }
        public bool Status { get; set; }
        [ValidDueDate]
        public DateTime DueDate { get; set; }
        public string? UserId { get; set; }
        public User? User { get; set; }
    }
}
