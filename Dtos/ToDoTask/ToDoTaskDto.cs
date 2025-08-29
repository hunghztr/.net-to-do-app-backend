using System.ComponentModel.DataAnnotations.Schema;
using ToDoList.Utils.Attribute;

namespace ToDoList.Dtos.ToDoTask
{
    public class ToDoTaskDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public DateTime DueDate { get; set; }
    }
}
