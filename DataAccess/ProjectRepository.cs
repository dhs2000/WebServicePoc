using System;

using DomainModel;

using NHibernate;

namespace DataAccess
{
    public class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
        public ProjectRepository(ISession session)
            : base(session)
        {
        }
    }
}