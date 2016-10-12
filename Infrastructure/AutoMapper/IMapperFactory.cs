using AutoMapper;

namespace Infrastructure.AutoMapper
{
    public interface IMapperFactory
    {
        IMapper Create();
    }
}