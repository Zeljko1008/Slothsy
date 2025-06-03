using System;

namespace Slothsy.Application.DTOs
{
    /// <summary>
    /// Data Transfer Object (DTO) for creating a new product.
    /// </summary>
    public class CreateProductDto
    {
        /// <summary>
        /// Name of the product.
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Optional description of the product.
        /// </summary>
        public string? Description { get; set; } 

        /// <summary>
        /// Price of the product.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Discounted price of the product, if any.
        /// </summary>
        public decimal? DiscountPrice { get; set; }

        /// <summary>
        /// Image URL of the product.
        /// </summary>
        public string ImageUrl { get; set; } = null!;

        /// <summary>
        /// Unique identifier for the category to which the product belongs.
        /// </summary>
        public Guid CategoryId { get; set; }

        /// <summary>
        /// Quantity of the product in stock.
        /// </summary>
        public int StockQuantity { get; set; }

        /// <summary>
        /// Stock Keeping Unit identifier.
        /// </summary>
        public string? Sku { get; set; } 

        /// <summary>
        /// Indicates whether the product is active.
        /// </summary>
        public bool IsActive { get; set; } = true;


        /// <summary>
        /// SEO title for the product, used for search engine optimization.
        /// </summary>
        public string? SeoTitle { get; set; }

        /// <summary>
        /// SEO description for the product, used for search engine optimization.
        /// </summary>
        public string? SeoDescription { get; set; }

        /// <summary>
        /// Slug for the product, used in URLs for better SEO and readability.
        /// </summary>
        public string? Slug { get; set; }
    }
}
