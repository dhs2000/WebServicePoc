using System;
using System.Diagnostics;
using System.Threading.Tasks;

using DataAccess;

using DomainModel;

using MediatR;

namespace ApplicationServices.Projects
{
    public class CreateProjectCommandHandler : IAsyncRequestHandler<CreateProjectCommand, Unit>
    {
        private readonly IProjectRepository projectRepository;

        private readonly IFileRepository fileRepository;

        public CreateProjectCommandHandler(IProjectRepository projectRepository, IFileRepository fileRepository)
        {
            if (projectRepository == null)
            {
                throw new ArgumentNullException(nameof(projectRepository));
            }

            if (fileRepository == null)
            {
                throw new ArgumentNullException(nameof(fileRepository));
            }

            this.projectRepository = projectRepository;
            this.fileRepository = fileRepository;
        }

        public Task<Unit> Handle(CreateProjectCommand message)
        {
            Debug.WriteLine(message);

            var project = new DomainModel.Project(Guid.NewGuid(), message.Name);
            project.AddItem(Guid.NewGuid(), "Item 1");
            project.AddItem(Guid.NewGuid(), "Item 2");

            var file = new File(Guid.NewGuid(), "File1");
            project.Items[1].LinkFile(file);

            this.fileRepository.Add(file);
            this.projectRepository.Add(project);

            return Task.FromResult(Unit.Value);
        }
    }
}