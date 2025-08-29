using AutoMapper;
using ToDoList.Dtos.Auth;
using ToDoList.Models;

namespace ToDoList.Mappers
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<RegisterDto, User>();
            CreateMap<LoginDto, User>();
        }
    }
}
