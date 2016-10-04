using System;
using System.Diagnostics;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc.Filters;

namespace WebServicePoc.Infrastructure
{
    public class DatabaseTransactionActionFilter : IAsyncActionFilter
    {

        public async Task OnActionExecutionAsync(
            ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            Debug.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
            try
            {
                // TODO: Stat tr
                await next();
                // TODO: Commit tr
            }
            catch (Exception e)
            {
                // TODO: Rollback tr
                throw;
            }
            finally
            {
                Debug.WriteLine("<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");
            }            
        }
    }
}