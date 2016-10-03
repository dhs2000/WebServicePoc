using System;

namespace DomainModel
{
    public class ProjectItem
    {
        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public Project Project { get; private set; }
    }
}