using Slothsy.Domain.Entities;
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

        public string? Slug { get; set; }

        public string? ProductColorVariantSlug { get; set; }

        public Guid ProductColorVariantId { get; set; }

        public Guid SizeOptionId { get; set; }

        public string SizeLabel { get; set; } = string.Empty;

        public int StockQuantity { get; set; }
    }
}
