using DomainModel;

using NHibernate;

namespace DataAccess
{
    public class FileRepository : BaseRepository<File>, IFileRepository
    {
        public FileRepository(ISession session)
            : base(session)
        {
        }
    }
}