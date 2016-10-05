﻿using Autofac;

using DataAccess;

using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;

using NHibernate;

namespace WebServicePoc.Infrastructure
{
    public class DataAccessModule : Module
    {
        private const string ConnectionString =
            "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=WebServicePoc.Db;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register<ISessionFactory>(i => CreateSessionFactory()).SingleInstance();
            builder.Register<ISession>(i => i.Resolve<ISessionFactory>().OpenSession()).InstancePerLifetimeScope();

            builder.RegisterType<IProjectRepository>().As<ProjectRepository>().InstancePerLifetimeScope();
        }

        private static ISessionFactory CreateSessionFactory()
        {
            return
                Fluently.Configure()
                    .Database(
                        MsSqlConfiguration.MsSql2012
                            .ConnectionString(ConnectionString)
                            .DefaultSchema("dbo")
                            .ShowSql())
                    .Mappings(m => m
                        .FluentMappings.AddFromAssemblyOf<ProjectMap>())
                    .BuildSessionFactory();
        }
    }
}