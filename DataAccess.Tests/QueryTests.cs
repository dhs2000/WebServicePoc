using System.Collections.Generic;

using DomainModel;

using NHibernate;

using NUnit.Framework;

namespace DataAccess.Tests
{
    [TestFixture]
    public class QueryTests
    {
        private NHibernateBootstrapper bootstrapper;

        public ISessionFactory SessionFactory { get; set; }

        [SetUp]
        public void SetUp()
        {
            this.bootstrapper = new NHibernateBootstrapper();

            this.SessionFactory = this.bootstrapper.SessionFactory;
        }

        [Test]
        public void SimpleQuery()
        {
            using (ISession session = this.SessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    Project projectAlias = null;
                    ProjectItem itemAlias = null;

                    IList<object[]> resultTable = session.QueryOver<Project>(() => projectAlias)
                        .JoinQueryOver<ProjectItem>(i => i.Items, () => itemAlias)
                        .Where(i => i.File != null)
                        .OrderBy(i => i.Name)
                        .Asc.Select(
                            i => projectAlias.Id,
                            i => projectAlias.Name,
                            i => itemAlias.Id,
                            i => itemAlias.Name)
                        .List<object[]>();

                    transaction.Commit();
                }
            }
        }

        [TearDown]
        public void TearDown()
        {
            this.bootstrapper.Dispose();
        }
    }
}