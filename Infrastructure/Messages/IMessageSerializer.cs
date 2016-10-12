using System.IO;

namespace Infrastructure.Messages
{
    public interface IMessageSerializer
    {
        void Serialize(Stream stream, object message);
    }
}