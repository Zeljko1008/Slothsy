using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slothsy.Application.DTOs
{
    public class ColorOptionDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? HexCode { get; set; }

        public string? ImageUrl { get; set; }

        public int Order { get; set; }
    }
}
