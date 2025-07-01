using Slothsy.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slothsy.Domain.Entities
{
    public class ProductVariant
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; } = null!;

        public Guid SizeOptionId { get; set; }
        public SizeOption SizeOption { get; set; } = null!;

        public Guid ColorOptionId { get; set; }
        public ColorOption ColorOption { get; set; } = null!;

        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }

        public int StockQuantity { get; set; }

        public ICollection<ProductVariantImage> Images { get; set; } = new List<ProductVariantImage>();
    }
}
