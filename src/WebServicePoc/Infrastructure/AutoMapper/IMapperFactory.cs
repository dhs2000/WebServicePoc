using AutoMapper;

namespace WebServicePoc.Infrastructure.AutoMapper
{
    public interface IMapperFactory
    {
        IMapper Create();
    }
}