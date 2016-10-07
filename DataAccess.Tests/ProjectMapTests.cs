using System;

using DomainModel;

using FluentAssertions;

using FluentNHibernate.Testing;

using NHibernate;

using NLog;

using NUnit.Framework;

namespace DataAccess.Tests
{
    [TestFixture]
    public class ProjectMapTests
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        private NHibernateBootstrapper bootstrapper;

        public ISessionFactory SessionFactory { get; private set; }

        [Test]
        public void DetectChangesInRootTest()
        {
            Guid id = Guid.NewGuid();
            const string NewName = "Project New Name";

            this.CreateProject(id);

            using (ISession session = this.SessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var project = session.Load<Project>(id);
                    project.UpdateProjectName(NewName);
                    project.Name.Should().Be(NewName);
                    transaction.Commit();
                }
            }
        }

        [Test]
        public void GeneralTest()
        {
            using (ISession session = this.SessionFactory.OpenSession())
            {
                new PersistenceSpecification<Project>(session).CheckProperty(c => c.Name, "pppp")
                    .CheckProperty(c => c.RootRevision, 23)
                    .VerifyTheMappings(new Project(Guid.NewGuid(), "Project 1"));
            }
        }

        [SetUp]
        public void SetUp()
        {
            this.bootstrapper = new NHibernateBootstrapper();

            this.SessionFactory = this.bootstrapper.SessionFactory;
        }

        [TearDown]
        public void TearDown()
        {
            this.bootstrapper.Dispose();
        }

        [Test]
        public void AddSubItemTest()
        {
            Guid id = Guid.NewGuid();

            this.CreateProject(id);

            using (ISession session = this.SessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var project = session.Get<Project>(id);

                    project.AddItem(Guid.NewGuid(), "Item3");

                    project.Items[1].File.Name.Should().Be("File1");
                    project.Items.Count.Should().Be(3);
                    transaction.Commit();
                }
            }
        }

        [Test]
        public void RemoveSubItemTest()
        {
            Guid id = Guid.NewGuid();

            this.CreateProject(id);

            using (ISession session = this.SessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var project = session.Get<Project>(id);
                    project.RemoveItem(project.Items[1].Id);
                    transaction.Commit();
                }
            }

            using (ISession session = this.SessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var project = session.Get<Project>(id);
                    project.Items.Count.Should().Be(1);
                    transaction.Commit();
                }
            }
        }

        [Test]
        public void UpdateSubItemTest()
        {
            Guid id = Guid.NewGuid();
            const string Sufix = "_Sufix";
            string newName = "Item1" + Sufix;
            int newRevision = 0;

            this.CreateProject(id);

            using (ISession session = this.SessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var project = session.Get<Project>(id);
                    newRevision = project.RootRevision + 1;
                    project.Items[0].AppendToName(Sufix);                    
                    transaction.Commit();
                }
            }

            using (ISession session = this.SessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var project = session.Get<Project>(id);
                    project.Items[0].Name.Should().Be(newName);
                    project.RootRevision.Should().Be(newRevision);
                    transaction.Commit();
                }
            }
        }

        private void CreateProject(Guid id)
        {
            using (ISession session1 = this.SessionFactory.OpenSession())
            {
                using (ITransaction transaction = session1.BeginTransaction())
                {
                    var project = new Project(id, "Project 1");
                    var file = new File(Guid.NewGuid(), "File1");
                    project.AddItem(Guid.NewGuid(), "Item1");
                    project.AddItem(Guid.NewGuid(), "Item2");
                    project.Items[1].LinkFile(file);
                    session1.Save(file);
                    session1.Save(project);
                    transaction.Commit();
                }
            }
        }
    }
}