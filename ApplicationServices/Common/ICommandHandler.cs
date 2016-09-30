using System.Threading.Tasks;

namespace ApplicationServices.Common
{
    public interface ICommandHandler<in T> where T : ICommand
    {
        Task Handle(T command);
    }
}