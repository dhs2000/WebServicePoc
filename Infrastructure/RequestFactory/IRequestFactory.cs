namespace Infrastructure.RequestFactory
{
    public interface IRequestFactory
    {
        dynamic CreateRequest(string name, dynamic body);
    }
}