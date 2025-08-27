using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ToDoList.Models;

namespace ToDoList.Utils
{
    public class CustomFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result is ObjectResult result)
            {
               
                ApiResponse res = new ApiResponse
                {
                    StatusCode = result.StatusCode ?? 200,
                    Data = result.Value,
                    Messsage = result.StatusCode >= 200 && result.StatusCode < 300 ? "Request Successful" : "Occurr..."
                };
                context.Result = new ObjectResult(res)
                {
                    StatusCode = result.StatusCode
                };
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }
}
