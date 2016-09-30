using System;
using System.Threading.Tasks;

using ApplicationServices.Common;

namespace ApplicationServices.Projects
{
    public class GetProjectsRequestHandler : IRequestHandler<GetProjectsRequest, GetProjectsResponse>
    {
        public Task<GetProjectsResponse> Handle(GetProjectsRequest request)
        {
            return
                Task.FromResult(
                    new GetProjectsResponse()
                        {
                            Items =
                                new[]
                                    {
                                        new Project() { Id = Guid.NewGuid(), Name = "Project 1" },
                                        new Project() { Id = Guid.NewGuid(), Name = "Project 2" }
                                    }
                        });
        }
    }
}