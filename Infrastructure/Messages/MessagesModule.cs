using Autofac;

using Contracts;

namespace Infrastructure.Messages
{
    public class MessagesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register<IMessageTypeRepository>(
                    i => new MessageTypeRepository(i.Resolve<IMessageTypesFinder>(), typeof(ContractsAssembly).Assembly))
                .SingleInstance();
            builder.RegisterType<MessageTypesFinder>().As<IMessageTypesFinder>();
            builder.RegisterType<MessageFactory>().As<IMessageFactory>();
            builder.RegisterType<MessageMapperAdapter>().As<IMapper>();
        }
    }
}