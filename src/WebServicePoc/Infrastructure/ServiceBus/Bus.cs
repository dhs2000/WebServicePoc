using System;
using System.Diagnostics;

using DomainModel.Common;

namespace WebServicePoc.Infrastructure.ServiceBus
{
    public class Bus : IBus
    {
        public void Send(params IEvent[] events)
        {
            if (events == null)
            {
                throw new ArgumentNullException(nameof(events));
            }

            foreach (IEvent @event in events)
            {
                Debug.WriteLine("Publish " + @event);
            }
        }
    }
}