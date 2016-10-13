using System;

namespace Infrastructure.Messages
{
    public interface IMapper
    {
        object Map(object body, Type sourceType, Type destinationType);
    }
}