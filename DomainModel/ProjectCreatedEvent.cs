using System;

namespace DomainModel
{
    public class ProjectCreatedEvent : Event
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
            return $"ProjectCreatedEvent {{ {nameof(this.EventId)}: {this.EventId}, {nameof(this.Id)}: {this.Id} }}";
        }
    }
}