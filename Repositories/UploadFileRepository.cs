using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ToDoList.Data;
using ToDoList.Dtos.UploadFile;
using ToDoList.Interfaces;
using ToDoList.Models;
using ToDoList.Utils;

namespace ToDoList.Repositories
{
    public class UploadFileRepository : IUploadFileRepository
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public UploadFileRepository(ApplicationDBContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UploadFileDto> Create(UploadedFile uploadedFile)
        {
            await _context.Files.AddAsync(uploadedFile);
            await _context.SaveChangesAsync();
            return _mapper.Map<UploadFileDto>(uploadedFile);
        }

        public async Task<UploadedFile> GetById(int id)
        {
            var file = await _context.Files.FirstOrDefaultAsync(f => f.Id == id);
            if(file == null) throw new ErrorException("file is not exist");
            return file;
        }
     
        public async Task<UploadFileDto> UploadFile(IFormFile file)
        {
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            string subFolder = "Others";
            if (file.ContentType.StartsWith("image/"))
            {
                subFolder = "Images";
            }
            else if (file.ContentType.StartsWith("video/"))
            {
                subFolder = "Videos";
            }
            else if (file.ContentType.StartsWith("audio/"))
            {
                subFolder = "Audios";
            }
            if (file == null || file.Length == 0) throw new ErrorException("quá trình upload thất bại");
            var finalPath = Path.Combine(basePath, subFolder);

            if (!Directory.Exists(finalPath)) Directory.CreateDirectory(finalPath);

            var fileName = Guid.NewGuid().ToString()+"-"+file.FileName;
            var filePath = Path.Combine(finalPath, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var relativePath = Path.Combine("Uploads/"+subFolder, fileName).Replace("\\", "/");
            UploadFileDto uploadFileDto = new UploadFileDto
            {
                FileName = relativePath,
                FileSize = Math.Round((double)file.Length / 1024, 2), // size in KB
                FileType = file.ContentType
            };
            return uploadFileDto;
        }
    }
}
