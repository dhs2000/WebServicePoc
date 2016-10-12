using System;

namespace Infrastructure.Logging
{
    public class CorrelationIdProvider : ICorrelationIdProvider
    {
        public CorrelationIdProvider(Guid correlationId)
        {
            this.CorrelationId = correlationId;
        }

        public Guid CorrelationId { get; }
    }
}