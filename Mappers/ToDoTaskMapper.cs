using AutoMapper;
using ToDoList.Dtos.ToDoTask;
using ToDoList.Models;

namespace ToDoList.Mappers
{
    public class ToDoTaskMapper : Profile
    {
        public ToDoTaskMapper()
        {
            CreateMap<ToDoTaskDto, ToDoTask>();
            CreateMap<ToDoTask,ToDoTaskDto>();
          
        }
    }
}
