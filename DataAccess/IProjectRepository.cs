using System;

using DomainModel;

namespace DataAccess
{
    public interface IProjectRepository
    {
        Project Get(Guid id);

        void Add(Project project);
    }
}