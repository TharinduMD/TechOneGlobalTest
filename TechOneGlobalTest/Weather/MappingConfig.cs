using AutoMapper;
using Weather.Core;
using Weather.Core.Dto;

namespace Weather
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<City, CityDto>().ReverseMap();
        }
    }
}
