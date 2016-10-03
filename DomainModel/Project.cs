using System;
using System.Collections.Generic;

namespace DomainModel
{
    public class Project
    {
        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public int RootRevision { get; private set; }

        public byte[] RowVersion { get; private set; }

        public ICollection<ProjectItem> Items { get; private set; }
    }
}
