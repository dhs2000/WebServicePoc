using System.Collections.Generic;

using DomainModel.Common;

namespace DataAccess.Tests
{
    public class EventsDispatcherStub : IEventsDispatcher
    {
        private readonly List<IEvent> events = new List<IEvent>();

        public IReadOnlyList<IEvent> Events => this.events;

        public void Publish(IEnumerable<IEvent> newEvents)
        {
            this.events.AddRange(newEvents);
        }
    }
}