using System;
using System.IO;
using System.Linq;
using System.Reflection;

using Infrastructure.Logging;
using Infrastructure.Messages;

using Microsoft.ServiceBus.Messaging;

namespace Infrastructure.ServiceBus
{
    public class BrokeredMessageFactory : IBrokeredMessageFactory
    {
        private readonly ICorrelationIdProvider correlationIdProvider;

        private readonly IMessageSerializer messageSerializer;

        public BrokeredMessageFactory(
            ICorrelationIdProvider correlationIdProvider,
            IMessageSerializer messageSerializer)
        {
            this.correlationIdProvider = correlationIdProvider;
            this.messageSerializer = messageSerializer;
        }

        public BrokeredMessage CreateMessage(object @event)
        {
            var memoryStream = new MemoryStream();

            this.messageSerializer.Serialize(memoryStream, @event);

            var brokeredMessage = new BrokeredMessage(memoryStream, true);
            brokeredMessage.CorrelationId = this.correlationIdProvider.CorrelationId.ToString("N");
            brokeredMessage.MessageId = FindMessageId(@event);
            brokeredMessage.ContentType = @event.GetType().FullName;
            brokeredMessage.Properties["MessageType"] = @event.GetType().FullName;

            return brokeredMessage;
        }

        private static string FindMessageId(object @event)
        {
            PropertyInfo propertyId =
                @event.GetType()
                    .GetProperties()
                    .Where(i => i.CanRead)
                    .FirstOrDefault(
                        i =>
                            i.Name.Equals("EventId", StringComparison.OrdinalIgnoreCase)
                            || i.Name.Equals("MessageId", StringComparison.OrdinalIgnoreCase));
            return propertyId?.GetMethod.Invoke(@event, new object[0])?.ToString();
        }
    }
}