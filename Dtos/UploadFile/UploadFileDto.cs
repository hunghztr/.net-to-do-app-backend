namespace ToDoList.Dtos.UploadFile
{
    public class UploadFileDto
    {
        public string FileName { get; set; } = string.Empty;
        public double FileSize { get; set; }
        public string FileType { get; set; } = string.Empty;

    }
}
