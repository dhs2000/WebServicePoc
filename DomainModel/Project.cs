using System;
using System.Collections.Generic;

namespace DomainModel
{
    public class Project
    {
        private readonly ICollection<ProjectItem> items = new List<ProjectItem>();

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

        public byte[] RowVersion { get; private set; }

        public ICollection<ProjectItem> Items
        {
            get
            {
                return this.items;
            }
        }

        public void AddItem(Guid id, string name)
        {
            this.items.Add(new ProjectItem(id, name, this));
        }
    }
}
