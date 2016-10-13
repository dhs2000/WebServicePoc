namespace Messages
{
    public interface IMessageFactory
    {
        dynamic CreateMessage(string name, dynamic body);
    }
}