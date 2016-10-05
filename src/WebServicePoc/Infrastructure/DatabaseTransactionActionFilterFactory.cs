using System;

using Microsoft.AspNetCore.Mvc.Filters;

using NHibernate;

namespace WebServicePoc.Infrastructure
{
    public class DatabaseTransactionActionFilterFactory : IFilterFactory
    {
        public bool IsReusable => false;

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            return new DatabaseTransactionActionFilter((ISession)serviceProvider.GetService(typeof(ISession)));
        }
    }
}