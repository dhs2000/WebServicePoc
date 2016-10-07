using System;

namespace DataAccess.Repositories
{
    public interface IRepository<T>
    {
        T Get(Guid id, bool forceLock = false);

        void Add(T project);
    }
}