using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DomainModel;

using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Testing;

using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

using NLog;

using NUnit.Framework;

namespace DataAccess.Tests
{
    [TestFixture]
    public class ProjectMapTests
    {
        private static readonly ILogger Logger = NLog.LogManager.GetCurrentClassLogger();

        private ISession session;

        [SetUp]
        public void SetUp()
        {
            Configuration configuration = Fluently.Configure()
                .Database(SQLiteConfiguration.Standard.InMemory)
                .Diagnostics(d => d.Enable(true).OutputToConsole())
                .Mappings(m =>
                    m.FluentMappings
                        .AddFromAssemblyOf<ProjectMap>())
                .BuildConfiguration();

            configuration.SetProperty("nhibernate-logger", "NHibernate.NLogLoggerFactory, NHibernate.NLog");
            configuration.SetProperty("show_sql", "true");


            ISessionFactory sessionFactory = configuration.BuildSessionFactory();

            this.session = sessionFactory.OpenSession();

            new SchemaExport(configuration).Execute(s => { }, true, false, this.session.Connection, Console.Out);

            Logger.Debug("Db session was created");
        }

        [TearDown]
        public void TearDown()
        {
            this.session.Dispose();
        }

        [Test]
        public void GeneralTest()
        {
            var project = new Project(Guid.NewGuid(), "Project 1");

            new PersistenceSpecification<Project>(this.session)
                .CheckProperty(c => c.Name, "pppp")
                .CheckProperty(c => c.RootRevision, 23)
                .VerifyTheMappings(project);
        }
    }
}
