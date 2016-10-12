using System.IO;

namespace WebServicePoc.Infrastructure.ServiceBus
{
    public interface IMessageSerializer
    {
        void Serialize(MemoryStream memoryStream, object message);
    }
}