using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Dtos;
using ToDoList.Interfaces;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly ITokenRepository _tokenRepository;
        private readonly SignInManager<User> _signInManager;

        public AuthController(ILogger<AuthController> logger, UserManager<User> userManager,
            IMapper mapper, ITokenRepository tokenRepository, SignInManager<User> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _mapper = mapper;
            _tokenRepository = tokenRepository;
            _signInManager = signInManager;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto request)
        {
            var user = _mapper.Map<User>(request);
            var result = await _userManager.CreateAsync(user,request.password);
            if (result.Succeeded)
            {
                var role = await _userManager.AddToRoleAsync(user, "User");
                if (role.Succeeded)
                {
                    return Ok(new
                    {
                        Username = user.UserName,
                        AccessToken = _tokenRepository.GenerateToken(user, "access"),
                        RefreshToken = _tokenRepository.GenerateToken(user, "refresh")
                    });
                }
                else
                {
                    return BadRequest(role.Errors);
                }
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto request)
        {
            var user = _mapper.Map<User>(request);
            var existingUser = await _userManager.FindByNameAsync(user.UserName);

            if(existingUser == null) return Unauthorized("Bad credentials");
            
            var result = await _signInManager.CheckPasswordSignInAsync(existingUser, request.password, false);
            if (result.Succeeded)
            {
                return Ok(new
                {
                    Username = existingUser.UserName,
                    AccessToken = _tokenRepository.GenerateToken(user, "access"),
                    RefreshToken = _tokenRepository.GenerateToken(user, "refresh")
                });
            }
                return Unauthorized("Bad credentials");
        }
    }
}
