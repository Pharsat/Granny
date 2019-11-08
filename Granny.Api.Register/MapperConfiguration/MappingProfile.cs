﻿using System;
using AutoMapper;
using Granny.DataModel;
using Granny.DataTransferObject.Price;

namespace Granny.Api.Register.ConfigurationMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PriceCreateDto, Location>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Location));
            CreateMap<PriceCreateDto, Product>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.PluCode));
            CreateMap<PriceCreateDto, Price>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.RegisterDate, opt => opt.MapFrom(src => DateTime.Now));
        }
    }
}
