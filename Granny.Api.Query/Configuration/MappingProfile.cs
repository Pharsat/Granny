using AutoMapper;
using Granny.DataModel;
using Granny.DataTransferObject.Price;

namespace Granny.Api.Query.Configuration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Price, PriceOutputDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.LocationName, opt => opt.MapFrom(src => src.Location.Name));
        }
    }
}
