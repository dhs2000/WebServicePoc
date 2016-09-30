using ApplicationServices.Common;

namespace ApplicationServices.Projects
{
    public class GetProjectsRequest : IQuery
    {
        public string Id { get; set; }
    }
}