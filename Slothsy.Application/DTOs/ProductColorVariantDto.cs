using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slothsy.Application.DTOs
{
    public class ProductColorVariantDto
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }
        public string ProductSlug { get; set; } = string.Empty;

        public Guid ColorOptionId { get; set; }

        public string ColorName { get; set; } = string.Empty;

        public string? Slug { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }

        public ICollection<ProductVariantDto> Variants { get; set; } = new List<ProductVariantDto>();

        public ICollection<ProductVariantImageDto> Images { get; set; } = new List<ProductVariantImageDto>();
    }
}
