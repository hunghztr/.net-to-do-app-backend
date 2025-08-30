using ToDoList.Dtos.UploadFile;
using ToDoList.Models;

namespace ToDoList.Interfaces
{
    public interface IUploadFileRepository
    {
        Task<UploadFileDto> UploadFile(IFormFile file);
        Task<UploadFileDto> Create(UploadedFile uploadedFile);
        Task<UploadedFile> GetById(int id);
    }
}
