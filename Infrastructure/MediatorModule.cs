using System.Collections.Generic;
using System.Linq;

using ApplicationServices;

using Autofac;
using Autofac.Core;
using Autofac.Features.Variance;

using Infrastructure.Transactions;
using Infrastructure.Validation;

using MediatR;

namespace Infrastructure
{
    public class MediatorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterSource(new ContravariantRegistrationSource());
            builder.RegisterAssemblyTypes(typeof(IMediator).Assembly).AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(typeof(ApplicationServicesAssembly).Assembly)
                .As(type => type.GetInterfaces()
                               .Where(interfaceType => TypeExtensions.IsClosedTypeOf(interfaceType, typeof(IAsyncRequestHandler<,>)))
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