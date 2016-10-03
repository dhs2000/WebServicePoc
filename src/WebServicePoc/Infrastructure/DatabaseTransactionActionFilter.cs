using System;
using System.Diagnostics;
using System.Threading.Tasks;

using DataAccess;

using Microsoft.AspNetCore.Mvc.Filters;

namespace WebServicePoc.Infrastructure
{
    public class DatabaseTransactionActionFilter : IAsyncActionFilter
    {
        private readonly DatabaseContext databaseContext;

        public DatabaseTransactionActionFilter(DatabaseContext databaseContext)
        {
            if (databaseContext == null)
            {
                throw new ArgumentNullException(nameof(databaseContext));
            }

            this.databaseContext = databaseContext;
        }

        public async Task OnActionExecutionAsync(
            ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            Debug.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
            this.databaseContext.BeginTransaction();
            try
            {
                await next();
                this.databaseContext.CloseTransaction();
            }
            catch (Exception e)
            {
                this.databaseContext.CloseTransaction(e);
                throw;
            }
            finally
            {
                Debug.WriteLine("<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");
            }            
        }
    }
}