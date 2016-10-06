using System;

using DomainModel;

namespace DataAccess
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