using System;

using AutoMapper;

namespace WebServicePoc.Infrastructure
{
    public class MapperFactory : IMapperFactory
    {
        private readonly MapperConfiguration mapperConfiguration;

        public MapperFactory(MapperConfiguration mapperConfiguration)
        {
            if (mapperConfiguration == null)
            {
                throw new ArgumentNullException(nameof(mapperConfiguration));
            }

            this.mapperConfiguration = mapperConfiguration;
        }

        public IMapper Create()
        {
            return this.mapperConfiguration.CreateMapper();
        }
    }
}