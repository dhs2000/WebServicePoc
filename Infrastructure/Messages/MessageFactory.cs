using System;

using AutoMapper;

using NLog;

namespace Infrastructure.Messages
{
    public class MessageFactory : IMessageFactory
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        private readonly IMapper mapper;

        private readonly IMessageTypeRepository messageTypeRepository;

        public MessageFactory(IMapper mapper, IMessageTypeRepository messageTypeRepository)
        {
            if (mapper == null)
            {
                throw new ArgumentNullException(nameof(mapper));
            }

            if (messageTypeRepository == null)
            {
                throw new ArgumentNullException(nameof(messageTypeRepository));
            }

            this.mapper = mapper;
            this.messageTypeRepository = messageTypeRepository;
        }

        public dynamic CreateMessage(string name, dynamic body)
        {
            if (Logger.IsDebugEnabled)
            {
                Logger.Debug("Create request '{0}'", name);
            }

            return this.mapper.Map(body, body.GetType(), this.messageTypeRepository.GetType(name));
        }
    }
}