using System.Threading.Tasks;

namespace WebServicePoc.Infrastructure.ServiceBus
{
    public interface IBus
    {
        Task PublishAsync(params object[] events);
    }
}