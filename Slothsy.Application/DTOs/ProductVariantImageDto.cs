using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slothsy.Application.DTOs
{
    public class ProductVariantImageDto
    {
        public Guid Id { get; set; }

        public string ImageUrl { get; set; } = string.Empty;

        public int Order { get; set; }

        public bool IsMain { get; set; }
    }
}
