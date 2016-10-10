using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Contracts;

using DomainModel;

using MediatR;

using NHibernate;
using NHibernate.Criterion;

using Project = Contracts.Project;

namespace ApplicationServices.Projects
{
    public class GetProjectsRequestHandler : IAsyncRequestHandler<GetProjectsRequest, GetProjectsResponse>
    {
        private readonly ISession session;

        public GetProjectsRequestHandler(ISession session)
        {
            if (session == null)
            {
                throw new ArgumentNullException(nameof(session));
            }

            this.session = session;
        }

        public Task<GetProjectsResponse> Handle(GetProjectsRequest request)
        {
            Guid id = request.Id == null ? Guid.Empty : new Guid(request.Id);

            DomainModel.Project projectAlias = null;
            ProjectItem projectItemAlias = null;

            IList<object[]> rows = this.session.QueryOver(() => projectAlias)
                .Where(i => (projectAlias.Id == id) || (id == Guid.Empty))
                .JoinQueryOver(i => i.Items, () => projectItemAlias)
                .OrderBy(i => projectAlias.Name)
                .Asc.Select(
                    Projections.ProjectionList()
                        .Add(Projections.Group<Project>(i => i.Id))
                        .Add(Projections.Group<Project>(i => i.Name))
                        .Add(Projections.Count<ProjectItem>(i => projectItemAlias.Id)))
                .List<object[]>();

            Project[] projects = rows
                    .Select(i => new Project() { Id = (Guid)i[0], Name = (string)i[1], ItemsCount = Convert.ToInt64(i[2]) })
                    .ToArray();

            return Task.FromResult(new GetProjectsResponse { Items = projects });
        }
    }
}