using System.Collections.Generic;

using DomainModel.Common;

namespace DataAccess
{
    public interface IEventsPublisher
    {
        void Publish(IEnumerable<IEvent> newEvents);
    }
}