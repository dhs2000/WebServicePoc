using Microsoft.ServiceBus.Messaging;

namespace WebServicePoc.Infrastructure.ServiceBus
{
    public interface IBrokeredMessageFactory
    {
        BrokeredMessage CreateMessage(object @event);
    }
}