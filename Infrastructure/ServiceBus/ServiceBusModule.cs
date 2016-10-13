using Autofac;

using DataAccess;

namespace Infrastructure.ServiceBus
{
    public class ServiceBusModule : Module
    {
        private const string BusConnectionString = "Endpoint=sb://obsqcgrintegrationservicebus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=e8NByiuua4pOY7TBSvHHCYUh54a82re3NsQsuxPajgQ=";

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MessageSerializer>().As<IMessageSerializer>();

            builder.RegisterType<ServiceBusEventsDispatcher>().As<IEventsDispatcher>().InstancePerLifetimeScope();

            builder.RegisterType<BrokeredMessageFactory>().As<IBrokeredMessageFactory>();

            builder.Register<IBus>(i => new AzureServiceBus(BusConnectionString, "Events-Test", i.Resolve<IBrokeredMessageFactory>())).SingleInstance();

            base.Load(builder);
        }
    }
}