using System;

namespace DomainModel.Common
{
    public interface IEvent
    {
        Guid EventId { get; }
    }
}