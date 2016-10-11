using System.Collections.Generic;

using DomainModel.Common;

namespace DataAccess
{
    public interface IEventsDispatcher
    {
        void Publish(IEnumerable<IEvent> newEvents);
    }
}