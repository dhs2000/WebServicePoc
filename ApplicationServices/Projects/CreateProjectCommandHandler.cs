using System;
using System.Diagnostics;
using System.Threading.Tasks;

using DataAccess;

using MediatR;

namespace ApplicationServices.Projects
{
    public class CreateProjectCommandHandler : IAsyncRequestHandler<CreateProjectCommand, Unit>
    {
        private readonly DatabaseContext databaseContext;

        public CreateProjectCommandHandler(DatabaseContext databaseContext)
        {
            if (databaseContext == null)
            {
                throw new ArgumentNullException(nameof(databaseContext));
            }

            this.databaseContext = databaseContext;
        }

        public Task<Unit> Handle(CreateProjectCommand message)
        {
            Debug.WriteLine("adadadadadadadad");
            Debug.WriteLine(message);

            var project = new DomainModel.Project(Guid.NewGuid(), "Project 1");
            project.AddItem(Guid.NewGuid(), "Item 1");
            this.databaseContext.Projects.Add(project);

            return Task.FromResult(Unit.Value);
        }
    }
}