using Autofac;

using WebServicePoc.Controllers;

namespace WebServicePoc.Infrastructure
{
    public class CommandQueryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RequestFactory>().As<IRequestFactory>();
        }
    }
}