using System;

namespace WebServicePoc.Infrastructure
{
    public interface IRequestTypeProvider
    {
        Type GetType(string name);
    }
}