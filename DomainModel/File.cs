using System;

using DomainModel.Common;

namespace DomainModel
{
    public class File : IAggregateRoot
    {
        private Guid id;

        private string name;

        public File(Guid id, string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            this.id = id;
            this.name = name;
        }

        protected File()
        {
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
    }
}