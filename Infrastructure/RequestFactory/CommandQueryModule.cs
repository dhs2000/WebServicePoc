using System;
using System.Collections.Generic;
using System.Linq;

using Autofac;

using Contracts;

using MediatR;

namespace Infrastructure.RequestFactory
{
    public class CommandQueryModule : Module
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

        protected override void Load(ContainerBuilder builder)
        {
            Type[] requestTypes = CommandTypes.Union(QueryTypes).ToArray();

            builder.Register(i => new RequestTypeProvider(requestTypes)).As<IRequestTypeProvider>().SingleInstance();
            builder.RegisterType<RequestFactory>().As<IRequestFactory>();
        }
    }
}