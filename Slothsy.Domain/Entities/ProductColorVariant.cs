using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slothsy.Domain.Entities
{
    public class ProductColorVariant
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; } = null!;

        public Guid ColorOptionId { get; set; }
        public ColorOption ColorOption { get; set; } = null!;

        public string? Slug { get; set; }

        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }

        public ICollection<ProductVariant> Variants { get; set; } = new List<ProductVariant>();

        public ICollection<ProductVariantImage> Images { get; set; } = new List<ProductVariantImage>();
    }
}
