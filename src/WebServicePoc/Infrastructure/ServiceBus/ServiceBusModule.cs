using Autofac;

using DataAccess;

namespace WebServicePoc.Infrastructure.ServiceBus
{
    public class ServiceBusModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ServiceBusEventsDispatcher>().As<IEventsDispatcher>().InstancePerLifetimeScope();

            builder.RegisterType<AzureServiceBus>().As<IBus>().SingleInstance();

            base.Load(builder);
        }
    }
}