using System;

namespace DomainModel
{
    public class File
    {
        public File(Guid id, string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            this.Id = id;
            this.Name = name;
        }

        public virtual Guid Id { get; private set; }        

        public virtual string Name { get; private set; }
        
        public virtual int RootRevision { get; private set; }
    }
}