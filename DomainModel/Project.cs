using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using DomainModel.Common;

namespace DomainModel
{
    public class Project : IAggregateRoot
    {
        private readonly IList<ProjectItem> items = new List<ProjectItem>();

        private string name;

        private Project()
        {
        }

        public Project(Guid id, string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            this.Id = id;
            this.Name = name;
        }

        public Guid Id { get; private set; }

        public string Name
        {
            get
            {
                return this.name;
            }
            private set
            {
                this.name = value;
            }
        }

        public int RootRevision { get; private set; }

        public IReadOnlyList<ProjectItem> Items => new ReadOnlyCollection<ProjectItem>(this.items);

        public void AddItem(Guid id, string itemName)
        {
            this.items.Add(new ProjectItem(id, itemName, this));
        }

        public void UpdateProjectName(string newName)
        {
            this.Name = newName;
        }
    }
}