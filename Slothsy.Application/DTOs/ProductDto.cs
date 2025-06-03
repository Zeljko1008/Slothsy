using System;

namespace Slothsy.Application.DTOs
{
    /// <summary>
    /// Data Transfer Object (DTO) for a product.
    /// </summary>
    public class ProductDto
    {
        /// <summary>
        /// Unique identifier for the product.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Name of the product.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Optional description of the product.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Price of the product.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Discounted price of the product, if available.
        /// </summary>
        public decimal? DiscountPrice { get; set; }

        /// <summary>
        /// Quantity of the product currently in stock.
        /// </summary>
        public int StockQuantity { get; set; }

        /// <summary>
        /// Unique SKU (Stock Keeping Unit) code to identify the product in inventory.
        /// </summary>
        public string Sku { get; set; } = string.Empty;

        /// <summary>
        /// Indicates whether the product is active and available for sale.
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Date and time when the product was created in the system.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Image URL of the product.
        /// </summary>
        public string? ImageUrl { get; set; }

        /// <summary>
        /// Unique identifier for the category to which the product belongs.
        /// </summary>
        public Guid CategoryId { get; set; }

        /// <summary>
        /// Category name of the product.
        /// </summary>
        public string? CategoryName { get; set; }

        /// <summary>
        /// SEO title for the product, used in page metadata for better search engine visibility.
        /// </summary>
        public string? SeoTitle { get; set; }

        /// <summary>
        /// SEO description for the product, used in page metadata to summarize the product for search engines.
        /// </summary>
        public string? SeoDescription { get; set; }

        /// <summary>
        /// SEO slug for the product, used in URLs to make them more readable and SEO-friendly.
        /// </summary>
        public string? Slug { get; set; }

        
    }
}
