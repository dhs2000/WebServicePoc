using System;

using MediatR;

namespace Contracts.Events
{
    public class ProjectCreatedEvent : IAsyncNotification
    {
        public ProjectCreatedEvent(Guid eventId, Guid id)
        {
            this.EventId = eventId;
            this.Id = id;
        }

        private ProjectCreatedEvent()
        {
        }

        public Guid EventId { get; }

        public Guid Id { get; }
    }
}