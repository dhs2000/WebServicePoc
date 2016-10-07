using System;
using System.Collections.Generic;
using System.Linq;

using ApplicationServices.Projects;

using Autofac;

using MediatR;

namespace WebServicePoc.Infrastructure
{
    public class CommandQueryModule : Module
    {
        private static IEnumerable<Type> CommandTypes
        {
            get
            {
                return
                    typeof(GetProjectsRequest).Assembly.GetTypes()
                        .Where(t => t.IsClass && !t.IsAbstract && typeof(IAsyncRequest).IsAssignableFrom(t))
                        .ToArray();
            }
        }

        private static IEnumerable<Type> QueryTypes
        {
            get
            {
                return
                    typeof(GetProjectsRequest).Assembly.GetTypes()
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