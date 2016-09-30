using System.Reflection;

using ApplicationServices.Projects;

using Autofac;

using FluentValidation;

using Module = Autofac.Module;

namespace WebServicePoc.Infrastructure
{
    public class FluentValidationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AutofacValidatorFactory>().As<IValidatorFactory>().SingleInstance();
            RegisterAssemblyValidators(builder, typeof(GetProjectsRequest).Assembly);
        }

        private static void RegisterAssemblyValidators(ContainerBuilder builder, Assembly assembly)
        {
            builder.RegisterAssemblyTypes(assembly)
                              .Where(t => t.Name.EndsWith("Validator"))
                              .AsImplementedInterfaces()
                              .InstancePerLifetimeScope();
        }
    }
}