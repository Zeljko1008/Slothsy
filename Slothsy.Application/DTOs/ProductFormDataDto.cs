using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slothsy.Application.DTOs
{
    /// <summary>
    /// Represents a data transfer object containing form data for product attributes.
    /// </summary>
    /// <remarks>This class provides collections of options for various product attributes, such as purposes,
    /// fits,  genders, age groups, materials, and seasons. Each collection is represented as a list of  <see
    /// cref="EnumOptionDto"/> objects, which encapsulate the available options for the corresponding
    /// attribute.</remarks>
    public class ProductFormDataDto
    {
        public List<EnumOptionDto> Purposes { get; set; } = new();
        public List<EnumOptionDto> Fits { get; set; } = new();
        public List<EnumOptionDto> Genders { get; set; } = new();
        public List<EnumOptionDto> AgeGroups { get; set; } = new();
        public List<EnumOptionDto> Materials { get; set; } = new();
        public List<EnumOptionDto> Seasons { get; set; } = new();
    }
}
