using System;

namespace Messages
{
    public interface IMapper
    {
        object Map(object body, Type sourceType, Type destinationType);
    }
}