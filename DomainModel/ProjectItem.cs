using System;

using DomainModel.Common;

namespace DomainModel
{
    public class ProjectItem : IAggregateRootProvider
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

        public File File { get; private set; }

        public string Name { get; private set; }

        public Project Project { get; private set; }

        public int Version { get; private set; }

        IAggregateRoot IAggregateRootProvider.AggregateRoot => this.Project;

        public void AppendToName(string itemsufix)
        {
            this.Name += itemsufix;
        }

        public void LinkFile(File file)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            this.File = file;
        }
    }
}