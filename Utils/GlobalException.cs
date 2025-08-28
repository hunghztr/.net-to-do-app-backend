using Microsoft.IdentityModel.Tokens;
using ToDoList.Models;

namespace ToDoList.Utils
{
    public class GlobalException
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalException> _logger;

        public GlobalException(RequestDelegate next, ILogger<GlobalException> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            } 
            catch (SecurityTokenException ex)
            {
                // Token invalid (signature sai, issuer sai...)
                _logger.LogWarning(ex, "Invalid token");
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                var response = new ApiResponse
                {
                    StatusCode = 401,
                    Data = null,
                    Message = "Token không hợp lệ"
                };

                await context.Response.WriteAsJsonAsync(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception");
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                var response = new ApiResponse
                {
                    StatusCode = 400,
                    Data = null,
                    Message = ex.Message
                };

                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
