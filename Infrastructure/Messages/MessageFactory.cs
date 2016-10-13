using System;

namespace Infrastructure.Messages
{
    public class MessageFactory : IMessageFactory
    {
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
            return this.mapper.Map(body, body.GetType(), this.messageTypeRepository.GetType(name));
        }
    }
}