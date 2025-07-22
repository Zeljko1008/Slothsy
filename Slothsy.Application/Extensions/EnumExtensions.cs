using Slothsy.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slothsy.Application.Extensions
{
    public static class EnumExtensions
    {
        public static List<EnumOptionDto> ToEnumDtoList(this List<KeyValuePair<int, string>> enumPairs)
        {
            return enumPairs.Select(p => new EnumOptionDto
            {
                Id = p.Key,
                Name = p.Value
            }).ToList();
        }
    }
}
