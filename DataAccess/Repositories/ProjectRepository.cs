using DomainModel;

using NHibernate;

namespace DataAccess.Repositories
{
    public class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
        public ProjectRepository(ISession session)
            : base(session)
        {
        }
    }
}