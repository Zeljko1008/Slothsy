using Slothsy.Domain.Enums;
using System;
using System.Reflection;

namespace Slothsy.Domain.Entities
{
    /// <summary>
    /// Represents a product listed in the shop.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Unique identifier of the product.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Display name of the product.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Short description of the product, used for quick reference or display purposes.
        /// </summary>
        public string? ShortDescription { get; set; }

        /// <summary>
        /// Detailed description of the product.
        /// </summary>
        public string? Description { get; set; }

           /// <summary>
        /// Intended style or usage of the product (e.g., sport, casual).
        /// </summary>
        public ProductPurpose Purpose { get; set; }

        /// <summary>
        /// Product fit or cut (e.g., slim, regular).
        /// </summary>
        public FitType Fit { get; set; }

        /// <summary>
        /// Brand or manufacturer of the product.
        /// </summary>
        public string Brand { get; set; } = string.Empty;

        /// <summary>
        /// Target gender for the product.
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// Target age group for the product.
        /// </summary>
        public AgeGroup AgeGroup { get; set; }

        /// <summary>
        /// Material from which the product is made.
        /// </summary>
        public MaterialType Material { get; set; }

        /// <summary>
        /// Intended season for the product (e.g. summer/winter).
        /// </summary>
        public Season Season { get; set; }

        /// <summary>
        /// Indicates whether the product is visible in the shop.
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Date when the product was added.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// SEO title for the product, used in page metadata for better search engine visibility.
        /// </summary>
        public string? SeoTitle { get; set; }

        /// <summary>
        /// SEO description for the product, used in page metadata to improve search engine ranking.
        /// </summary>
        public string? SeoDescription { get; set; }

        /// <summary>
        /// SEO-friendly URL slug for the product, used in links and page titles.
        /// </summary>
        public string? Slug { get; set; }

        /// <summary>
        /// Timestamp of the last update to the product.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Gets or sets the collection of product categories associated with the current entity.
        /// </summary>
        public ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();

        /// <summary>
        /// List of all variants (e.g. different sizes or colors) of this product.
        /// </summary>
        public ICollection<ProductVariant> Variants { get; set; } = new List<ProductVariant>();
    }
}
