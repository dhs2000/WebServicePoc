using System;

using DomainModel;

using FluentAssertions;

using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Testing;

using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Util;

using NLog;

using NUnit.Framework;

namespace DataAccess.Tests
{
    [TestFixture]
    public class ProjectMapTests
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        private ISession session;

        private ISessionFactory sessionFactory;

        [Test]
        public void ProjectWithSubItemsTest()
        {
            Guid id = Guid.NewGuid();        

            const string NewName = "Project New Name";

            using (ITransaction transaction = this.session.BeginTransaction())
            {
                var project = new Project(id, "Project 1");
                project.AddItem(Guid.NewGuid(), "Item1");
                project.AddItem(Guid.NewGuid(), "Item2");
                this.session.Save(project);
                transaction.Commit();
            }

            this.session.Clear();

            using (ITransaction transaction = this.session.BeginTransaction())
            {
                var project = this.session.Get<Project>(id);
                project.AddItem(Guid.NewGuid(), "Item3");
                project.Items.Count.Should().Be(3);
                transaction.Commit();
            }

            this.session.Clear();

            using (ITransaction transaction = this.session.BeginTransaction())
            {
                var project = this.session.Get<Project>(id);
                ProjectItem first = project.Items[0];
                first.AppendToName("ItemSufix");
                transaction.Commit();
            }

            this.session.Clear();

            using (ITransaction transaction = this.session.BeginTransaction())
            {
                var project = this.session.Get<Project>(id);                
                project.Items.Count.Should().Be(3);
                project.Items[0].Name.Should().Be("Item1ItemSufix");
                transaction.Commit();
            }
        }

        [Test]
        public void DetectChangesTest()
        {
            Guid id = Guid.NewGuid();
            const string NewName = "Project New Name";

            this.CreateProject(id);

            using (ITransaction transaction = this.session.BeginTransaction())
            {
                var project = this.session.Load<Project>(id);
                project.UpdateProjectName(NewName);
                project.Name.Should().Be(NewName);
                transaction.Commit();
            }

            this.session.Clear();

            using (ITransaction transaction = this.session.BeginTransaction())
            {
                var project = this.session.Load<Project>(id);
                project.Name.Should().Be(NewName);
                transaction.Commit();
            }
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

        [SetUp]
        public void SetUp()
        {
            Configuration configuration =
                Fluently.Configure()
                    .Database(SQLiteConfiguration.Standard.InMemory)
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
                                c.SetProperty("nhibernate-logger", "DataAccess.Tests.NLogLoggerFactory, DataAccess.Tests");
                            })
                    .BuildConfiguration();

            configuration.ProtectMyDomainModelFromDomainDrivenDesignIgnorance();
            configuration.AddDddListeners();

            this.sessionFactory = configuration.BuildSessionFactory();

            this.session = this.sessionFactory.OpenSession();

            new SchemaExport(configuration).Execute(s => { }, true, false, this.session.Connection, Console.Out);

            Logger.Debug("Db session was created");
        }

        [TearDown]
        public void TearDown()
        {
            this.session.Dispose();
        }

        private void CreateProject(Guid id)
        {
            using (ITransaction transaction = this.session.BeginTransaction())
            {
                var project = new Project(id, "Project 1");
                this.session.Save(project);
                transaction.Commit();
            }

            this.session.Clear();
        }
    }
}