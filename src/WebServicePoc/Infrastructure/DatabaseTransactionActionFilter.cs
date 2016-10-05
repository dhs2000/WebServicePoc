using System;
using System.Data;
using System.Diagnostics;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc.Filters;

using NHibernate;

namespace WebServicePoc.Infrastructure
{
    public class DatabaseTransactionActionFilter : IAsyncActionFilter
    {
        private readonly ISession session;

        public DatabaseTransactionActionFilter(ISession session)
        {
            if (session == null)
            {
                throw new ArgumentNullException(nameof(session));
            }

            this.session = session;
        }

        public async Task OnActionExecutionAsync(
            ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            Debug.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
            try
            {
                this.session.BeginTransaction(IsolationLevel.ReadCommitted);
                await next();
                this.session.Transaction.Commit();
            }
            catch (Exception)
            {
                this.session.Transaction.Rollback();
                throw;
            }
            finally
            {
                Debug.WriteLine("<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");
            }            
        }
    }
}