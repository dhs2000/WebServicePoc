using System.IO;

namespace Infrastructure.ServiceBus
{
    public interface IMessageSerializer
    {
        void Serialize(Stream stream, object message);
    }
}