using System.IO;

using Newtonsoft.Json;

namespace Infrastructure.ServiceBus
{
    public class MessageSerializer : IMessageSerializer
    {
        public void Serialize(Stream stream, object message)
        {
            JsonSerializer.Create().Serialize(new StreamWriter(stream), message);
        }
    }
}