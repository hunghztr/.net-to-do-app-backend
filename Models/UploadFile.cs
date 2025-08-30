using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoList.Models
{
    [Table("UploadFiles")]
    public class UploadedFile
    {
        public int Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public double FileSize { get; set; }
        public string FileType { get; set; } = string.Empty;
        public DateTime UploadDate { get; set; } = DateTime.Now;
        public string? UploaderId { get; set; } = string.Empty;
        public User? Uploader { get; set; }
    }
}
