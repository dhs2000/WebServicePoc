using Microsoft.ServiceBus.Messaging;

namespace Infrastructure.ServiceBus
{
    public interface IBrokeredMessageFactory
    {
        BrokeredMessage CreateMessage(object @event);
    }
}