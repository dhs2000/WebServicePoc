using System;

using DomainModel;

namespace DataAccess.Repositories
{
    public interface IRepository<T>
    {
        T Get(Guid id, bool forceLock = false);

        void Add(T project);
    }

    public interface IProjectRepository : IRepository<Project>
    {
    }
}