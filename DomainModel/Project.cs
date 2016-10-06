using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using DomainModel.Common;

namespace DomainModel
{
    public class Project : IAggregateRoot
    {
        private readonly IList<ProjectItem> items = new List<ProjectItem>();

        private Guid id;

        private string name;

        protected Project()
        {
        }

        public Project(Guid id, string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            this.id = id;
            this.name = name;
        }

        public virtual Guid Id
        {
            get
            {
                return this.id;
            }
            protected set
            {
                this.id = value;
            }
        }

        public virtual string Name
        {
            get
            {
                return this.name;
            }
            protected set
            {
                this.name = value;
            }
        }

        public virtual int RootRevision { get; protected set; }

        public virtual IReadOnlyList<ProjectItem> Items => new ReadOnlyCollection<ProjectItem>(this.items);

        public virtual void AddItem(Guid projectItemId, string itemName)
        {
            this.items.Add(new ProjectItem(projectItemId, itemName, this));
        }

        public virtual void RemoveItem(Guid projectItemId)
        {
            ProjectItem item = this.items.FirstOrDefault(i => i.Id == projectItemId);
            if (item == null)
            {
                throw new InvalidOperationException();
            }

            this.items.Remove(item);
        }

        public virtual void UpdateProjectName(string newName)
        {
            this.Name = newName;
        }
    }
}