using System;

using DomainModel.Common;

namespace DomainModel
{
    public class ProjectCreatedEvent : IEvent
    {
        public ProjectCreatedEvent(Guid id)
        {
            this.Id = id;
        }

        private ProjectCreatedEvent()
        {
        }

        public Guid Id { get; }

        public override string ToString()
        {
            return $"ProjectCreatedEvent {{ {nameof(this.Id)}: {this.Id} }}";
        }
    }
}