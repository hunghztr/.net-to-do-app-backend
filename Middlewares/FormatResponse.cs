using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ToDoList.Utils;

namespace ToDoList.Middlewares
{
    public class FormatResponse : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result is ObjectResult result)
            {

                ApiResponse res = new ApiResponse
                {
                    StatusCode = result.StatusCode ?? 200,
                    Data = result.Value,
                    Message = result.StatusCode >= 200 && result.StatusCode < 300 ? "Request Successful" : "Occurr..."
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
