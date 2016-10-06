using System;

using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;

using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace DataAccess.Tests
{
    public sealed class NHibernateBootstrapper : IDisposable
    {
        public NHibernateBootstrapper()
        {
            Configuration configuration = CreateConfiguration();

            this.SessionFactory = configuration.BuildSessionFactory();

            this.Session = this.SessionFactory.OpenSession();

            new SchemaExport(configuration).Execute(s => { }, true, false, this.Session.Connection, Console.Out);
        }

        public ISessionFactory SessionFactory { get; private set; }

        private ISession Session { get; set; }

        public void Dispose()
        {
            this.Session?.Dispose();
        }

        private static Configuration CreateConfiguration()
        {
            Configuration configuration =
                Fluently.Configure()
                    .Database(
                        SQLiteConfiguration.Standard.ConnectionString(
                            "FullUri=file:memorydb.db?mode=memory&cache=shared"))
                    .Diagnostics(d => d.Enable(true).OutputToConsole())
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<ProjectMap>())
                    .ExposeConfiguration(
                        c =>
                            {
                                c.DataBaseIntegration(
                                    i =>
                                        {
                                            i.AutoCommentSql = true;
                                            i.LogFormattedSql = true;
                                            i.LogSqlInConsole = true;
                                        });
                                /*
                                                                c.SetProperty(
                                                                    "nhibernate-logger",
                                                                    "DataAccess.Tests.NLogLoggerFactory, DataAccess.Tests");
                                */
                                c.AddDddListeners();
                            }).BuildConfiguration();

            return configuration;
        }
    }
}