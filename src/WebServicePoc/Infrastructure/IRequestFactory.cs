namespace WebServicePoc.Infrastructure
{
    public interface IRequestFactory
    {
        dynamic CreateRequest(string commandName, dynamic body);
    }
}