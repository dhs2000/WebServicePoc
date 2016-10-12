using System;

namespace Infrastructure.Messages
{
    public interface IMessagetTypeProvider
    {
        Type GetType(string name);
    }
}