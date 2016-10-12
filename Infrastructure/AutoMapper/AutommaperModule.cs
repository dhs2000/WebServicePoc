using Autofac;

using AutoMapper;

namespace Infrastructure.AutoMapper
{
    public class AutommaperModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(i => new MapperConfiguration(cfg => cfg.CreateMissingTypeMaps = true)).SingleInstance();
            builder.RegisterType<MapperFactory>().As<IMapperFactory>().SingleInstance();
            builder.Register(i => i.Resolve<IMapperFactory>().Create());
        }
    }
}