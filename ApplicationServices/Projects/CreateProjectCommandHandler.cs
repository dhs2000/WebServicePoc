using System;
using System.Diagnostics;
using System.Threading.Tasks;

using DataAccess;

using MediatR;

namespace ApplicationServices.Projects
{
    public class CreateProjectCommandHandler : IAsyncRequestHandler<CreateProjectCommand, Unit>
    {
        private readonly IProjectRepository projectRepository;

        public CreateProjectCommandHandler(IProjectRepository projectRepository)
        {
            if (projectRepository == null)
            {
                throw new ArgumentNullException(nameof(projectRepository));
            }

            this.projectRepository = projectRepository;
        }

        public Task<Unit> Handle(CreateProjectCommand message)
        {
            Debug.WriteLine(message);

            var project = new DomainModel.Project(Guid.NewGuid(), message.Name);
            project.AddItem(Guid.NewGuid(), "Item 1");

            this.projectRepository.Save(project);

            return Task.FromResult(Unit.Value);
        }
    }
}