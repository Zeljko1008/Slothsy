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

        public string? Slug { get; set; }

        public string? ProductColorVariantSlug { get; set; }

        public Guid ProductColorVariantId { get; set; }
        public ProductColorVariant ProductColorVariant { get; set; } = null!;

        public Guid SizeOptionId { get; set; }
        public SizeOption SizeOption { get; set; } = null!;
        public string SizeLabel { get; set; } = string.Empty;

        public int StockQuantity { get; set; }

    }
}
