using AutoMapper;

namespace WebServicePoc.Infrastructure
{
    public interface IMapperFactory
    {
        IMapper Create();
    }
}