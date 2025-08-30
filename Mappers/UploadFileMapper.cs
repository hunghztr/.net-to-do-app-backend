using AutoMapper;
using ToDoList.Dtos.UploadFile;
using ToDoList.Models;

namespace ToDoList.Mappers
{
    public class UploadFileMapper : Profile
    {
        public UploadFileMapper()
        {
            CreateMap<UploadedFile, UploadFileDto>();
        }
    }
}
