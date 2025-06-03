using System;

namespace Slothsy.Application.DTOs
{
    /// <summary>
    /// Data Transfer Object (DTO) for updating an existing product.
    /// </summary>
    public class UpdateProductDto
    {
        /// <summary>
        /// Name of the product.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Optional description of the product.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Price of the product.
        /// </summary>
        public decimal? Price { get; set; }

        /// <summary>
        /// Discounted price of the product, if any.
        /// </summary>
        public decimal? DiscountPrice { get; set; }

        /// <summary>
        /// Image URL of the product.
        /// </summary>
        public string? ImageUrl { get; set; }

        /// <summary>
        /// Unique identifier for the category to which the product belongs.
        /// </summary>
        public Guid? CategoryId { get; set; }

        /// <summary>
        /// Quantity of the product in stock.
        /// </summary>
        public int? StockQuantity { get; set; }

        /// <summary>
        /// Stock Keeping Unit identifier.
        /// </summary>
        public string? Sku { get; set; }

        /// <summary>
        /// Indicates whether the product is active.
        /// </summary>
        public bool? IsActive { get; set; }


        /// <summary>
        /// Search engine optimization title for the product.
        /// </summary>
        public string? SeoTitle { get; set; }

        /// <summary>
        /// Search engine optimization description for the product.
        /// </summary>
        public string? SeoDescription { get; set; }

        /// <summary>
        /// Slug for the product, used in URLs.
        /// </summary>
        public string? Slug { get; set; }
    }
}
