using System;

namespace Infrastructure.RequestFactory
{
    public interface IRequestTypeProvider
    {
        Type GetType(string name);
    }
}