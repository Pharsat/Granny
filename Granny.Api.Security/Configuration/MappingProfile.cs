﻿using System;
using AutoMapper;
using Granny.DataModel;
using Granny.DataTransferObject.Price;
using Granny.DataTransferObject.User;

namespace Granny.Api.Security.Configuration
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

            CreateMap<UserCreateDto, User>();

            CreateMap<User, UserOutputDto>();
        }
    }
}