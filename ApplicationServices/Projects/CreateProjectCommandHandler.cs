using System;
using System.Threading.Tasks;

using Contracts;

using DataAccess.Repositories;

using DomainModel;

using MediatR;

using NLog;

namespace ApplicationServices.Projects
{
    public class CreateProjectCommandHandler : IAsyncRequestHandler<CreateProjectCommand, Unit>
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        private readonly IFileRepository fileRepository;

        private readonly IProjectRepository projectRepository;

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
            Logger.Debug("Creating project  - {0}", message);

            var project = new DomainModel.Project(new Guid(message.Id), message.Name);
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