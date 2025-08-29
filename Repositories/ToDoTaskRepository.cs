using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ToDoList.Data;
using ToDoList.Dtos.Paging;
using ToDoList.Dtos.ToDoTask;
using ToDoList.Interfaces;
using ToDoList.Models;
using ToDoList.Utils;

namespace ToDoList.Repositories
{
    public class ToDoTaskRepository : IToDoTaskRepository
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;
        public ToDoTaskRepository(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<ToDoTaskDto> Create(ToDoTask task)
        {
            await _context.ToDoTasks.AddAsync(task);
            await _context.SaveChangesAsync();
            return _mapper.Map<ToDoTaskDto>(task);
        }

        public async Task<ToDoTaskDto> Delete(int id)
        {
            var task = await _context.ToDoTasks.FirstOrDefaultAsync(t => t.Id == id);
            if (task == null)
            {
                return null;
            }
            _context.ToDoTasks.Remove(task);
            await _context.SaveChangesAsync();
            return _mapper.Map<ToDoTaskDto>(task);
        }

        public async Task<ResultDto<ToDoTaskDto>> GetAll(QueryObject query)
        {
            var queryable = _context.ToDoTasks.AsQueryable();
            int skip = (query.CurrentPage - 1) * query.PageSize;
            var tasks = await queryable.Skip(skip).Take(query.PageSize).ToListAsync();
            var meta = new Meta
            {
                CurrentPage = query.CurrentPage,
                PageSize = query.PageSize,
                TotalCounts =  queryable.Count(),
                TotalPages = (int)Math.Ceiling((double)queryable.Count() / query.PageSize)
            };
            var tasksDto = tasks.Select(t => _mapper.Map<ToDoTaskDto>(t)).ToList();
            return new ResultDto<ToDoTaskDto>
            {
                Datas = tasksDto,
                Meta = meta
            };
        }

        public async Task<ResultDto<ToDoTaskDto>> GetAllByUsername(User user, QueryObject query)
        {

            var queryable = _context.ToDoTasks.AsQueryable();
            int skip = (query.CurrentPage - 1) * query.PageSize;
            var tasks =  await queryable.Where(t => t.UserId == user.Id)
                .Skip(skip).Take(query.PageSize).ToListAsync();
            var meta = new Meta
            {
                CurrentPage = query.CurrentPage,
                PageSize = query.PageSize,
                TotalCounts = queryable.Count(),
                TotalPages = (int)Math.Ceiling((double)queryable.Count() / query.PageSize)
            };
            var tasksDto = tasks.Select(t => _mapper.Map<ToDoTaskDto>(t)).ToList();
            return new ResultDto<ToDoTaskDto>
            {
                Datas = tasksDto,
                Meta = meta
            };
        }

        public async Task<ToDoTaskDto> GetById(int id)
        {
            var task = await _context.ToDoTasks.FirstOrDefaultAsync(t => t.Id == id);
            if(task == null) return null;
            return _mapper.Map<ToDoTaskDto>(task);
        }

        public async Task<ToDoTaskDto> Update(ToDoTask entity, int id)
        {
            var task = await _context.ToDoTasks.FirstOrDefaultAsync(t => t.Id == id);
            if (task == null) return null;
            task.Title = entity.Title;
            task.Description = entity.Description;
            task.Status = entity.Status;
            task.DueDate = entity.DueDate;
            await _context.SaveChangesAsync();
            return _mapper.Map<ToDoTaskDto>(task);
        }
    }
}
