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
            catch (SecurityTokenExpiredException ex)
            {
                // Token hết hạn
                _logger.LogWarning(ex, "Token expired");
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                var response = new ApiResponse
                {
                    StatusCode = 401,
                    Data = null,
                    Message = "Token đã hết hạn"
                };

                await context.Response.WriteAsJsonAsync(response);
            }
            catch (SecurityTokenMalformedException ex)
            {
                _logger.LogWarning(ex, "Token malformed");
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                var response = new ApiResponse
                {
                    StatusCode = 401,
                    Data = null,
                    Message = "Token sai định dạng"
                };
                await context.Response.WriteAsJsonAsync(response);
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
