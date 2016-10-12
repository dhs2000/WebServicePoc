using System;

using DomainModel.Common;

namespace DomainModel
{
    public class Event : IEvent
    {
        protected Event()
        {
            this.EventId = Guid.NewGuid();
        }

        public Guid EventId { get; }
    }
}