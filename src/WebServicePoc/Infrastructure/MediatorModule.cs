using System.Collections.Generic;
using System.Linq;

using ApplicationServices;
using ApplicationServices.Projects;

using Autofac;
using Autofac.Core;
using Autofac.Features.Variance;

using MediatR;

using WebServicePoc.Infrastructure.Transactions;
using WebServicePoc.Infrastructure.Validation;

namespace WebServicePoc.Infrastructure
{
    public class MediatorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterSource(new ContravariantRegistrationSource());
            builder.RegisterAssemblyTypes(typeof(IMediator).Assembly).AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(typeof(ApplicationServicesAssembly).Assembly)
                .As(type => type.GetInterfaces()
                               .Where(interfaceType => interfaceType.IsClosedTypeOf(typeof(IAsyncRequestHandler<,>)))
                               .Select(interfaceType => new KeyedService("AsyncRequestHandler", interfaceType)))
                .InstancePerLifetimeScope();

            builder.RegisterGenericDecorator(
                typeof(AsyncValidationRequestHandler<,>),
                typeof(IAsyncRequestHandler<,>),
                "AsyncRequestHandler",
                "TransactionRequestHandlerDecorator")
                .InstancePerLifetimeScope();

            builder.RegisterGenericDecorator(
                typeof(TransactionRequestHandlerDecorator<,>),
                typeof(IAsyncRequestHandler<,>),
                "TransactionRequestHandlerDecorator")
                .InstancePerLifetimeScope();

            builder.Register<SingleInstanceFactory>(
                ctx =>
                    {
                        var c = ctx.Resolve<IComponentContext>();
                        return t => c.Resolve(t);
                    });

            builder.Register<MultiInstanceFactory>(
                ctx =>
                    {
                        var c = ctx.Resolve<IComponentContext>();
                        return t => (IEnumerable<object>)c.Resolve(typeof(IEnumerable<>).MakeGenericType(t));
                    });
        }
    }
}