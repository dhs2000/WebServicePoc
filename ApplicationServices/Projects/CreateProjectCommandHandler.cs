using System.Diagnostics;
using System.Threading.Tasks;

using MediatR;

namespace ApplicationServices.Projects
{
    public class CreateProjectCommandHandler : IAsyncRequestHandler<CreateProjectCommand, Unit>
    {
        public Task<Unit> Handle(CreateProjectCommand message)
        {
            Debug.WriteLine(message);
            return Task.FromResult(Unit.Value);
        }
    }
}