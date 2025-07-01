using Slothsy.Domain.Enums;
using System;

namespace Slothsy.Application.DTOs
{
    /// <summary>
    /// Data Transfer Object (DTO) for product data.
    /// </summary>
    public class ProductDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? ShortDescription { get; set; }

        public string? Description { get; set; }

        public ProductPurpose Purpose { get; set; }

        public FitType Fit { get; set; }

        public string Brand { get; set; } = string.Empty;

        public Gender Gender { get; set; }

        public AgeGroup AgeGroup { get; set; }

        public MaterialType Material { get; set; }

        public Season Season { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

      
        public string? SeoTitle { get; set; }

        public string? SeoDescription { get; set; }

        public string? Slug { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public List<CategorySummaryDto> Categories { get; set; } = new();

        public List<ProductVariantDto> Variants { get; set; } = new();
    }
}
