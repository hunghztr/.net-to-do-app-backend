using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Interfaces;
using ToDoList.Models;
using ToDoList.Utils;

namespace ToDoList.Controllers
{
    [ApiController]
    [Route("api/uploadfiles")]
    public class UploadFileController : ControllerBase
    {
        private readonly ILogger<UploadFileController> _logger;
        private readonly IUploadFileRepository _uploadFileRepository;
        private readonly UserManager<User> _userManager;

        public UploadFileController(ILogger<UploadFileController> logger, IUploadFileRepository uploadFileRepository,
            UserManager<User> userManager)
        {
            _logger = logger;
            _uploadFileRepository = uploadFileRepository;
            _userManager = userManager;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            var uploadFileDto = await _uploadFileRepository.UploadFile(file);
            return Ok(uploadFileDto);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] UploadedFile uploadedFile)
        {
            var username = User.FindFirst("name")?.Value;
            var user = await _userManager.FindByNameAsync(username);
            if (user == null) throw new ErrorException("unauthorized");
            uploadedFile.UploaderId = user.Id;
            var uploadFileDto = await _uploadFileRepository.Create(uploadedFile);
            return Ok(uploadFileDto);
        }
        [HttpPost("download/{id}")]
        [Authorize]
        public async Task<IActionResult> DownloadFile([FromRoute] int id)
        {
            var file = await _uploadFileRepository.GetById(id);

            if (file == null || !System.IO.File.Exists(file.FileName)) throw new ErrorException("file is not exist");
         

            var memory = new MemoryStream();
            using (var stream = new FileStream(file.FileName, FileMode.Open, FileAccess.Read))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            // Trả file về cho client
            return File(memory, "application/octet-stream", file.FileName);
        }
    }
}
