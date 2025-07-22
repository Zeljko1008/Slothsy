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
    public class ProductColorVariantProfile :Profile
    {
        public ProductColorVariantProfile()
        {
            CreateMap<ProductColorVariant, ProductColorVariantDto>()
    .ForMember(dest => dest.ProductSlug, opt => opt.MapFrom(src => src.Product.Slug))
            .ForMember(dest => dest.ColorName, opt => opt.MapFrom(src => src.ColorOption.Name));
            CreateMap<ProductColorVariantDto, ProductColorVariant>();

            CreateMap<CreateProductColorVariantDto, ProductColorVariant>()
           .ForMember(dest => dest.Slug, opt => opt.Ignore()) 
           .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images))
           .ForMember(dest => dest.Variants, opt => opt.Ignore())
           .ForMember(dest => dest.ProductId, opt => opt.Ignore())
           .ForMember(dest => dest.Product, opt => opt.Ignore())
           .ForMember(dest => dest.ColorOption, opt => opt.Ignore());

            CreateMap<CreateVariantImageDto, ProductVariantImage>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ProductColorVariantId, opt => opt.Ignore())
                .ForMember(dest => dest.IsMain, opt => opt.Ignore()); 
        }


    }
}
