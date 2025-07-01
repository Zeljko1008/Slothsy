using AutoMapper;
using Slothsy.Application.DTOs;
using Slothsy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Slothsy.Application.Mappings
{
    public class ProductProfile :Profile
    {
       public ProductProfile()
        {
            // Map from Product entity to ProductDto
            CreateMap<Product, ProductDto>()
                 .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.ProductCategories.Select(pc => pc.Category)))
                .ForMember(dest => dest.Variants, opt => opt.MapFrom(src => src.Variants));
               


            // Map from CreateProductDto to Product entity (for creation)
            CreateMap<CreateProductDto, Product>();

            CreateMap<Category, CategorySummaryDto>();



        }
    }
}
