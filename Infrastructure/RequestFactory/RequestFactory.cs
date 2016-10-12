using System;

using AutoMapper;

using NLog;

namespace Infrastructure.RequestFactory
{
    public class RequestFactory : IRequestFactory
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        private readonly IMapper mapper;

        private readonly IRequestTypeProvider requestTypeProvider;

        public RequestFactory(IMapper mapper, IRequestTypeProvider requestTypeProvider)
        {
            if (mapper == null)
            {
                throw new ArgumentNullException(nameof(mapper));
            }

            if (requestTypeProvider == null)
            {
                throw new ArgumentNullException(nameof(requestTypeProvider));
            }

            this.mapper = mapper;
            this.requestTypeProvider = requestTypeProvider;
        }

        public dynamic CreateRequest(string name, dynamic body)
        {
            if (Logger.IsDebugEnabled)
            {
                Logger.Debug("Create request '{0}'", name);
            }

            return this.mapper.Map(body, body.GetType(), this.requestTypeProvider.GetType(name));
        }
    }
}