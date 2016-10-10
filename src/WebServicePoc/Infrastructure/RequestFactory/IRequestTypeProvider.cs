using System;

namespace WebServicePoc.Infrastructure.RequestFactory
{
    public interface IRequestTypeProvider
    {
        Type GetType(string name);
    }
}