using System;

using DomainModel;

using NHibernate;

namespace DataAccess
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ISession session;

        public ProjectRepository(ISession session)
        {
            if (session == null)
            {
                throw new ArgumentNullException(nameof(session));
            }

            this.session = session;
        }

        public Project Get(Guid id)
        {
            return this.session.Get<Project>(id);
        }

        public void Add(Project project)
        {
            this.session.Save(project);
        }
    }
}