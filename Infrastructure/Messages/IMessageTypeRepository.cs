using System;

namespace Infrastructure.Messages
{
    public interface IMessageTypeRepository
    {
        Type GetType(string name);
    }
}