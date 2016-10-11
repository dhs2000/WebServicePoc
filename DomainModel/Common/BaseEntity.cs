using System.Collections.Generic;

namespace DomainModel.Common
{
    public class BaseEntity : IEventsProvider
    {
        private readonly List<IEvent> events = new List<IEvent>();

        public virtual IEnumerable<IEvent> Events => this.events.AsReadOnly();

        public virtual void ResetEvents()
        {
            this.events.Clear();
        }

        protected virtual void RaiseEvent(IEvent @event)
        {
            this.events.Add(@event);
        }
    }
}