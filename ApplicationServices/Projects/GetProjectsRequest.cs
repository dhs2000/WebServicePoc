using MediatR;

namespace ApplicationServices.Projects
{
    public class GetProjectsRequest : IAsyncRequest<GetProjectsResponse>
    {
        public string Id { get; set; }
    }
}