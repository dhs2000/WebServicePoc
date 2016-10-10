using System;
using System.Data;
using System.Diagnostics;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc.Filters;

using NHibernate;

namespace WebServicePoc.Infrastructure.Transactions
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

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            Debug.WriteLine(">> DatabaseTransactionActionFilter");

            this.session.BeginTransaction(IsolationLevel.ReadCommitted);

            ActionExecutedContext executedContext = await next();

            if (executedContext.Exception == null)
            {
                Debug.WriteLine(">> DatabaseTransactionActionFilter.Commit");
                this.session.Transaction.Commit();
            }
            else
            {
                Debug.WriteLine(">> DatabaseTransactionActionFilter.Rollback");
                this.session.Transaction.Rollback();
            }
        }
    }
}