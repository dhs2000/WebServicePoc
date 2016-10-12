using System;

namespace WebServicePoc.Infrastructure.ServiceBus
{
    public interface ICorrelationIdProvider
    {
        Guid CorrelationId { get; }
    }
}