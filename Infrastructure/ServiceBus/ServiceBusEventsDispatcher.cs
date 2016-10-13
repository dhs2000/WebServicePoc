using System;
using System.Collections.Generic;
using System.Linq;

using DataAccess;

using DomainModel.Common;

using Infrastructure.Messages;

using Messages;

using NLog;

namespace Infrastructure.ServiceBus
{
    public class ServiceBusEventsDispatcher : IEventsDispatcher
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        private readonly IBus bus;

        private readonly IMessageFactory messageFactory;

        public ServiceBusEventsDispatcher(IBus bus, IMessageFactory messageFactory)
        {
            if (bus == null)
            {
                throw new ArgumentNullException(nameof(bus));
            }

            if (messageFactory == null)
            {
                throw new ArgumentNullException(nameof(messageFactory));
            }

            this.bus = bus;
            this.messageFactory = messageFactory;
        }

        public void Publish(IEnumerable<IEvent> newEvents)
        {
            if (newEvents == null)
            {
                throw new ArgumentNullException(nameof(newEvents));
            }

            object[] messages = newEvents.Select(this.Map).ToArray();

            this.bus.PublishAsync(messages).ContinueWith(t => 
                {
                    if (t.IsFaulted)
                    {
                        Logger.Error(t.Exception, "Error dispatching events. {0}", t.Exception?.Message);
                    }
                });
        }

        private object Map(IEvent @event)
        {
            return this.messageFactory.CreateMessage(@event.GetType().Name, @event);
        }
    }
}
