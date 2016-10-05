using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DomainModel;

using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Testing;

using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

using NUnit.Framework;

namespace DataAccess.Tests
{
    [TestFixture]
    public class ProjectMapTests
    {
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

            ISessionFactory sessionFactory = configuration.BuildSessionFactory();

            this.session = sessionFactory.OpenSession();

            new SchemaExport(configuration).Execute(s => { }, true, false, this.session.Connection, Console.Out);
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
                .CheckProperty(c => c.Name, project.Name)
                .CheckProperty(c => c.RootRevision, project.RootRevision)
                .VerifyTheMappings(project);
        }
    }
}
