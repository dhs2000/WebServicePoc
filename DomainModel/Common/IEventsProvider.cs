using System.Collections.Generic;

namespace DomainModel.Common
{
    public interface IEventsProvider
    {
        IEnumerable<IEvent> Events { get; }

        void ResetEvents();
    }
}