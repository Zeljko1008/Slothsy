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
    public class ColorOptionProfile : Profile
    {
        public ColorOptionProfile()
        {
            CreateMap<ColorOption, ColorOptionDto>();
            CreateMap<ColorOptionDto, ColorOption>();
        }
    }
}
