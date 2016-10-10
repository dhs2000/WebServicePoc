namespace WebServicePoc.Infrastructure
{
    public interface IRequestFactory
    {
        dynamic CreateRequest(string name, dynamic body);
    }
}