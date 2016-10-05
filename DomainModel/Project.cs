using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DomainModel
{
    public class Project
    {
        private readonly IList<ProjectItem> items = new List<ProjectItem>();

        private Project()
        {
        }

        public Project(Guid id, string name)
        {
            this.Id = id;
            this.Name = name;
            this.RootRevision = 1;
        }

        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public int RootRevision { get; private set; }

        public IReadOnlyCollection<ProjectItem> Items => new ReadOnlyCollection<ProjectItem>(this.items);

        public void AddItem(Guid id, string name)
        {
            this.items.Add(new ProjectItem(id, name, this));
        }
    }
}
