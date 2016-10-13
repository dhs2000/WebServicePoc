using System;

using IAutoMapper = AutoMapper.IMapper;

namespace Infrastructure.Messages
{
    public class MessageMapperAdapter : IMapper
    {
        private readonly IAutoMapper mapper;

        public MessageMapperAdapter(IAutoMapper mapper)
        {
            if (mapper == null)
            {
                throw new ArgumentNullException(nameof(mapper));
            }

            this.mapper = mapper;
        }

        public object Map(object body, Type sourceType, Type destinationType)
        {
            return this.mapper.Map(body, sourceType, destinationType);
        }
    }
}