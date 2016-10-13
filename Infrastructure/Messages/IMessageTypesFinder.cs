using System;
using System.Collections.Generic;
using System.Reflection;

namespace Infrastructure.Messages
{
    public interface IMessageTypesFinder
    {
        IEnumerable<Type> GetCommandTypes(Assembly assembly);

        IEnumerable<Type> GetQueryTypes(Assembly assembly);

        IEnumerable<Type> GetEventTypes(Assembly assembly);
    }
}