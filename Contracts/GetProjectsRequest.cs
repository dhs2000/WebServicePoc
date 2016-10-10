using MediatR;

namespace Contracts
{
    public class GetProjectsRequest : IAsyncRequest<GetProjectsResponse>
    {
        public string Id { get; set; }
    }
}