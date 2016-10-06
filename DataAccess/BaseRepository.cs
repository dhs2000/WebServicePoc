using System;

using NHibernate;

namespace DataAccess
{
    public class BaseRepository<T>
    {
        private readonly ISession session;

        protected BaseRepository(ISession session)
        {
            if (session == null)
            {
                throw new ArgumentNullException(nameof(session));
            }

            this.session = session;
        }

        public T Get(Guid id, bool forceLock)
        {
            return this.session.Get<T>(id, forceLock ? LockMode.Force : LockMode.None);
        }

        public void Add(T entity)
        {
            this.session.Save(entity);
        }
    }
}