using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing.Template;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ToDoList.Data;
using ToDoList.Models;

namespace ToDoList.Utils
{
    public class Interceptor : IAsyncActionFilter
    {
        private readonly ILogger<Interceptor> _logger;
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDBContext _context;
        private readonly List<(string Path, string Method)> _whiteList = new List<(string, string)>
        {
            new("/api/auth/login","Post"),
            new("/api/auth/register","Post"),
            new("api/auth/refresh","Post"),
            new("/swagger/index.html","Get"),

        };
        public Interceptor(ILogger<Interceptor> logger, UserManager<User> userManager, ApplicationDBContext context)
        {
            _logger = logger;
            _userManager = userManager;
            _context = context;
        }

        public bool IsMatch(string template, string actualPath)
        {
            var routeTemplate = TemplateParser.Parse(template); // vd: "api/{id}"
            var matcher = new TemplateMatcher(routeTemplate, new RouteValueDictionary());

            var values = new RouteValueDictionary();
            return matcher.TryMatch(actualPath, values);
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var username = context.HttpContext.User.FindFirst("name")?.Value;
            var method = context.HttpContext.Request.Method;
            var path = context.HttpContext.Request.Path.Value?.ToLower();
            bool isExist = _whiteList
                .Any(w => path.StartsWith(w.Path.ToLower()) && w.Method.ToLower() == method.ToLower());
            if (isExist)
            {
                await next();
                return;
            }
            var user = await _userManager.FindByNameAsync(username);
            if (user == null) throw new ErrorException("unauthorized");

            _logger.LogInformation("User: {Username} , Method: {Method}, Path: {Path}", username, method, path);
            var roles = await _userManager.GetRolesAsync(user);

            var permissions = await _context.RolePermissions
                .Where(rp => roles.Contains(rp.Role.Name))
                .Select(rp => rp.Permission).ToListAsync();
            bool allowAccess = permissions
                .Any(p => p.Method.ToLower() == method.ToLower() && IsMatch(p.Path.ToLower(), path.ToLower()));
            if (!allowAccess)
            {
                throw new ErrorException("forbidden");
            }
            await next();
        }
    }
}
