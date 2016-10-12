using System.IO;

namespace Infrastructure.ServiceBus
{
    public interface IMessageSerializer
    {
        void Serialize(MemoryStream memoryStream, object message);
    }
}