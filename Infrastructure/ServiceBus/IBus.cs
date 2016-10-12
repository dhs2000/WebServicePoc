using System.Threading.Tasks;

namespace Infrastructure.ServiceBus
{
    public interface IBus
    {
        Task PublishAsync(params object[] events);
    }
}