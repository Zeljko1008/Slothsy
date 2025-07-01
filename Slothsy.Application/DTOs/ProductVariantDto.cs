using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slothsy.Application.DTOs
{
    public class ProductVariantDto
    {
        public Guid Id { get; set; }

        public Guid SizeOptionId { get; set; }
        public string SizeLabel { get; set; } = string.Empty;

        public Guid ColorOptionId { get; set; }
        public string ColorName { get; set; } = string.Empty;
        public string? ColorHex { get; set; }

        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }

        public int StockQuantity { get; set; }

        public List<ProductVariantImageDto> Images { get; set; } = new();
    }
}
