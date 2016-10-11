using DomainModel.Common;

namespace WebServicePoc.Infrastructure.ServiceBus
{
    public interface IBus
    {
        void Send(params IEvent[] events);
    }
}