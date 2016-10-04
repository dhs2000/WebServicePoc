using System;

using Microsoft.AspNetCore.Mvc.Filters;

namespace WebServicePoc.Infrastructure
{
    public class DatabaseTransactionActionFilterFactory : IFilterFactory
    {
        public bool IsReusable => false;

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            return new DatabaseTransactionActionFilter();
        }
    }
}