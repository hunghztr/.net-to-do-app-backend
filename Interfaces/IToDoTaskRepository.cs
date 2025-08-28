using Microsoft.AspNetCore.Mvc;
using ToDoList.Dtos.Paging;
using ToDoList.Dtos.ToDoTask;
using ToDoList.Models;
using ToDoList.Utils;

namespace ToDoList.Interfaces
{
    public interface IToDoTaskRepository
    {
        Task<ToDoTaskDto> Create(ToDoTask task);
        Task<ToDoTaskDto> Update(ToDoTask task,int id);
        Task<ToDoTaskDto> Delete(int id);
        Task<ResultDto<ToDoTaskDto>> GetAll(QueryObject query);
        Task<ToDoTaskDto> GetById(int id);
    }
}
