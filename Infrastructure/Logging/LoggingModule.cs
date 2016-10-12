using System;

using Autofac;

using Infrastructure.ServiceBus;

namespace Infrastructure.Logging
{
    public class LoggingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register<ICorrelationIdProvider>(i => new CorrelationIdProvider(Guid.NewGuid())).InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}