using System;

namespace Infrastructure.ServiceBus
{
    public interface ICorrelationIdProvider
    {
        Guid CorrelationId { get; }
    }
}