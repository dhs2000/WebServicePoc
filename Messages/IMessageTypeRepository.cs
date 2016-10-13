using System;

namespace Messages
{
    public interface IMessageTypeRepository
    {
        Type GetType(string name);
    }
}