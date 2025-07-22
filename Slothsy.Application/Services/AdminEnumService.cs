using Slothsy.Application.DTOs;
using Slothsy.Application.Extensions;
using Slothsy.Application.Interfaces;
using Slothsy.Common.Helpers;
using Slothsy.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slothsy.Application.Services
{
    public class AdminEnumService : IAdminEnumService
    {
        public ProductFormDataDto GetAllProductFormEnums()
        {
            return new ProductFormDataDto
            {
                Purposes = EnumHelpers.GetEnumValues<ProductPurpose>().ToEnumDtoList(),
                Fits = EnumHelpers.GetEnumValues<FitType>().ToEnumDtoList(),
                Genders = EnumHelpers.GetEnumValues<Gender>().ToEnumDtoList(),
                AgeGroups = EnumHelpers.GetEnumValues<AgeGroup>().ToEnumDtoList(),
                Materials = EnumHelpers.GetEnumValues<MaterialType>().ToEnumDtoList(),
                Seasons = EnumHelpers.GetEnumValues<Season>().ToEnumDtoList()
            };
        }
    }
}
