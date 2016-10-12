using System;

using AutoMapper;

using NLog;

namespace Infrastructure.Messages
{
    public class MessageFactory : IMessageFactory
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        private readonly IMapper mapper;

        private readonly IMessagetTypeProvider messagetTypeProvider;

        public MessageFactory(IMapper mapper, IMessagetTypeProvider messagetTypeProvider)
        {
            if (mapper == null)
            {
                throw new ArgumentNullException(nameof(mapper));
            }

            if (messagetTypeProvider == null)
            {
                throw new ArgumentNullException(nameof(messagetTypeProvider));
            }

            this.mapper = mapper;
            this.messagetTypeProvider = messagetTypeProvider;
        }

        public dynamic CreateMessage(string name, dynamic body)
        {
            if (Logger.IsDebugEnabled)
            {
                Logger.Debug("Create request '{0}'", name);
            }

            return this.mapper.Map(body, body.GetType(), this.messagetTypeProvider.GetType(name));
        }
    }
}