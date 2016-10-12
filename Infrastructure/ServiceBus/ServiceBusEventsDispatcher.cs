using System;
using System.Collections.Generic;
using System.Linq;

using DataAccess;

using DomainModel.Common;

namespace Infrastructure.ServiceBus
{
    public class ServiceBusEventsDispatcher : IEventsDispatcher
    {
        private readonly IBus bus;

        public ServiceBusEventsDispatcher(IBus bus)
        {
            if (bus == null)
            {
                throw new ArgumentNullException(nameof(bus));
            }

            this.bus = bus;
        }

        public void Publish(IEnumerable<IEvent> newEvents)
        {
            if (newEvents == null)
            {
                throw new ArgumentNullException(nameof(newEvents));
            }

            this.bus.PublishAsync(newEvents.Select(this.Map).ToArray());
        }

        private object Map(IEvent @event)
        {
            return @event;
        }
    }
}
