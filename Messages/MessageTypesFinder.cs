using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using MediatR;

namespace Messages
{
    public class MessageTypesFinder : IMessageTypesFinder
    {
        public IEnumerable<Type> GetCommandTypes(Assembly assembly)
        {
            return
                assembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract && typeof(IAsyncRequest).IsAssignableFrom(t));
        }

        public IEnumerable<Type> GetQueryTypes(Assembly assembly)
        {
            return from x in assembly.GetTypes()
                   let y = x.BaseType
                   where
                   !x.IsAbstract && !x.IsInterface && x.IsClass && (y != null) && y.IsGenericType
                   && (y.GetGenericTypeDefinition() == typeof(IAsyncRequest<>))
                   select x;
        }

        public IEnumerable<Type> GetEventTypes(Assembly assembly)
        {
            return
                assembly.GetTypes()
                    .Where(t => t.IsClass && !t.IsAbstract && typeof(IAsyncNotification).IsAssignableFrom(t));
        }
    }
}