using System;

namespace DomainModel
{
    public class ProjectItem
    {
        private ProjectItem()
        {
        }

        public ProjectItem(Guid id, string name, Project project)
        {
            if (project == null)
            {
                throw new ArgumentNullException(nameof(project));
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
            }

            this.Id = id;
            this.Name = name;
            this.Project = project;
        }

        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public Project Project { get; private set; }
    }
}