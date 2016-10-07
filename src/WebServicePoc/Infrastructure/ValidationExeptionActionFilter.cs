using System.Diagnostics;
using System.Threading.Tasks;

using FluentValidation;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebServicePoc.Infrastructure
{
    public class ValidationExeptionActionFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            Debug.WriteLine($">> Validation filter");
            ActionExecutedContext executedContext = await next();
            var ex = executedContext.Exception as ValidationException;
            if (ex != null)
            {
                Debug.WriteLine($"Validation error - {ex.Message}");
                context.Result = new BadRequestObjectResult(ex.Errors);
                executedContext.ExceptionHandled = true;
                executedContext.Result = new BadRequestObjectResult(ex.Errors);
            }

            Debug.WriteLine($"<< Validation filter");
        }
    }
}