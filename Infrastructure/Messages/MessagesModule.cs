using System;
using System.Collections.Generic;
using System.Linq;

using Autofac;

using Contracts;

using MediatR;

namespace Infrastructure.Messages
{
    public class MessagesModule : Module
    {
        private static IEnumerable<Type> CommandTypes
        {
            get
            {
                return
                    typeof(ContractsAssembly).Assembly.GetTypes()
                        .Where(t => t.IsClass && !t.IsAbstract && typeof(IAsyncRequest).IsAssignableFrom(t))
                        .ToArray();
            }
        }

        private static IEnumerable<Type> QueryTypes
        {
            get
            {
                return
                    typeof(ContractsAssembly).Assembly.GetTypes()
                        .Where(t => t.IsClass && !t.IsAbstract && t.IsClosedTypeOf(typeof(IAsyncRequest<>)))
                        .ToArray();
            }
        }

        private static IEnumerable<Type> EventTypes
        {
            get
            {
                return
                    typeof(ContractsAssembly).Assembly.GetTypes()
                        .Where(t => t.IsClass && !t.IsAbstract && typeof(IAsyncNotification).IsAssignableFrom(t))
                        .ToArray();
            }
        }

        protected override void Load(ContainerBuilder builder)
        {
            Type[] requestTypes = CommandTypes.Union(QueryTypes).Union(EventTypes).ToArray();

            builder.Register(i => new MessageTypeProvider(requestTypes)).As<IMessagetTypeProvider>().SingleInstance();
            builder.RegisterType<MessageFactory>().As<IMessageFactory>();
            builder.RegisterType<MessageSerializer>().As<IMessageSerializer>();
        }
    }
}