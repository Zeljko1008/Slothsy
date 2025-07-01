using AutoMapper;
using Slothsy.Application.DTOs;
using Slothsy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slothsy.Application.Mappings
{
    public class ProductVariantProfile : Profile
    {
        public ProductVariantProfile()
        {
            CreateMap<ProductVariant, ProductVariantDto>()
    .ForMember(dest => dest.SizeOptionId, opt => opt.MapFrom(src => src.SizeOptionId))
    .ForMember(dest => dest.SizeLabel, opt => opt.MapFrom(src => src.SizeOption.Label))
    .ForMember(dest => dest.ColorOptionId, opt => opt.MapFrom(src => src.ColorOptionId))
    .ForMember(dest => dest.ColorName, opt => opt.MapFrom(src => src.ColorOption.Name))
    .ForMember(dest => dest.ColorHex, opt => opt.MapFrom(src => src.ColorOption.HexCode))
    .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images));

            CreateMap<ProductVariantDto, ProductVariant>();
        }
    }
}
