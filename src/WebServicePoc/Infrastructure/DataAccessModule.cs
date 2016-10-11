using Autofac;

using DataAccess;
using DataAccess.NHibernate;

using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;

using NHibernate;
using NHibernate.Cfg;

namespace WebServicePoc.Infrastructure
{
    public class DataAccessModule : Module
    {
        private const string ConnectionString =
            "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=WebServicePoc.Db;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register<ISessionFactory>(i => CreateSessionFactory()).SingleInstance();
            builder.RegisterType<DomainEventsIntercepter>().As<IInterceptor>().InstancePerLifetimeScope();
            builder.Register<ISession>(i => i.Resolve<ISessionFactory>().OpenSession(i.Resolve<IInterceptor>())).InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(DataAccessAssembly).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }

        private static ISessionFactory CreateSessionFactory()
        {
            return
                Fluently.Configure()
                    .Database(
                        MsSqlConfiguration.MsSql2012.ConnectionString(ConnectionString).DefaultSchema("dbo").ShowSql())
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<DataAccessAssembly>())
                    .ExposeConfiguration(
                        c =>
                            {
                                c.DataBaseIntegration(
                                    i =>
                                        {
                                            i.AutoCommentSql = true;
                                            i.LogFormattedSql = true;

                                            // i.LogSqlInConsole = true;
                                        });
                                /*
                                                                c.SetProperty(
                                                                    "nhibernate-logger",
                                                                    "DataAccess.Tests.NLogLoggerFactory, DataAccess.Tests");
                                */
                                c.AddDddListeners();
                            }).BuildConfiguration().BuildSessionFactory();
        }
    }
}