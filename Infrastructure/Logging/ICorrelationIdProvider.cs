using System;

namespace Infrastructure.Logging
{
    public interface ICorrelationIdProvider
    {
        Guid CorrelationId { get; }
    }
}