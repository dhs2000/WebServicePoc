using System;

using ApplicationServices.Projects;

using AutoMapper;

namespace WebServicePoc.Infrastructure
{
    public class RequestFactory : IRequestFactory
    {
        private readonly IMapper mapper;

        public RequestFactory(IMapper mapper)
        {
            if (mapper == null)
            {
                throw new ArgumentNullException(nameof(mapper));
            }

            this.mapper = mapper;
        }

        public dynamic CreateRequest(string commandName, dynamic body)
        {
            if (string.Equals(commandName, "CreateProject", StringComparison.OrdinalIgnoreCase))
            {
                dynamic request = this.mapper.Map(body, body.GetType(), typeof(CreateProjectCommand));
                return request;
            }

            if (string.Equals(commandName, "GetProjects", StringComparison.OrdinalIgnoreCase))
            {
                dynamic request = this.mapper.Map(body, body.GetType(), typeof(GetProjectsRequest));
                return request;
            }

            throw new ArgumentException($"Undefined command '{commandName}'", nameof(commandName));
        }
    }
}