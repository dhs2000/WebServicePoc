using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DomainModel;

using FluentAssertions;

using NHibernate;

using NUnit.Framework;

namespace DataAccess.Tests
{
    [TestFixture]
    public class DomainEventsTests
    {
        private NHibernateBootstrapper bootstrapper;

        public ISessionFactory SessionFactory { get; set; }

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
        public void ShouldPublishEventsOnCommit()
        {
            var eventsPublisherStub = new EventsDispatcherStub();

            using (ISession session = this.SessionFactory.OpenSession(new DomainEventsIntercepter(eventsPublisherStub)))
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var project = new Project(Guid.NewGuid(), "Pr1");

                    project.AddItem(Guid.NewGuid(), "Item1");

                    session.Save(project);

                    transaction.Commit();
                }
            }

            eventsPublisherStub.Events.Count.Should().Be(1);
        }

        [Test]
        public void ShouldNotPublishEventsOnRollback()
        {
            var eventsPublisherStub = new EventsDispatcherStub();

            using (ISession session = this.SessionFactory.OpenSession(new DomainEventsIntercepter(eventsPublisherStub)))
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var project = new Project(Guid.NewGuid(), "Pr1");

                    project.AddItem(Guid.NewGuid(), "Item1");

                    session.Save(project);

                    transaction.Rollback();
                }
            }

            eventsPublisherStub.Events.Count.Should().Be(0);
        }
    }
}
