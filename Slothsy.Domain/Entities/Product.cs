using System;

namespace Slothsy.Domain.Entities
{
    /// <summary>
    /// Represents a product available in the store.
    /// </summary>
    public class Product
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
        /// Detailed description of the product.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// The selling price of the product.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Optional discounted price if product is on sale.
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
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// URL to the product's image.
        /// </summary>
        public string ImageUrl { get; set; } = string.Empty;

        /// <summary>
        /// Foreign key referencing the category this product belongs to.
        /// </summary>
        public Guid CategoryId { get; set; }

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

        /// <summary>
        /// Navigation property for the product's category.
        /// </summary>
        public Category Category { get; set; } = null!;
    }
}
