using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Autofac;

using MediatR;

namespace Infrastructure.Messages
{
    public class MessageTypesFinder : IMessageTypesFinder
    {
        public IEnumerable<Type> GetCommandTypes(Assembly assembly)
        {
            return assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && typeof(IAsyncRequest).IsAssignableFrom(t));
        }

        public IEnumerable<Type> GetQueryTypes(Assembly assembly)
        {
            return assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.IsClosedTypeOf(typeof(IAsyncRequest<>)));
        }

        public IEnumerable<Type> GetEventTypes(Assembly assembly)
        {
            return assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && typeof(IAsyncNotification).IsAssignableFrom(t));
        }
    }
}