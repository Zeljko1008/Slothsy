using Slothsy.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slothsy.Domain.Entities
{
    /// <summary>
    /// Represents a product category with hierarchical structure.
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Primary key for the category.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Display name.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Gets or sets the gender for the category.
        /// </summary>
        public Gender? Gender { get; set; } 

        /// <summary>
        /// Gets or sets the age group classification for the category.
        /// </summary>
        public AgeGroup? AgeGroup { get; set; } 

        /// <summary>
        /// Full description.
        /// </summary>
        [MaxLength(500)]
        public string Description { get; set; } = null!;

        /// <summary>
        /// Optional short description.
        /// </summary>
        [MaxLength(300)]
        public string? ShortDescription { get; set; }

        /// <summary>
        /// Flag whether category is active.
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Banner image URL for homepage or category listing.
        /// </summary>
        [MaxLength(500)]
        public string? BannerImageUrl { get; set; }

        /// <summary>
        /// SEO slug used in URLs.
        /// </summary>
        [MaxLength(100)]
        public string? Slug { get; set; }

        /// <summary>
        /// SEO title.
        /// </summary>
        [MaxLength(70)]
        public string? SeoTitle { get; set; }

        /// <summary>
        /// SEO description.
        /// </summary>
        [MaxLength(160)]
        public string? SeoDescription { get; set; }

        /// <summary>
        /// Order of the category in listings.
        /// </summary>
        public int Order { get; set; } = 0;

        /// <summary>
        /// Foreign key to parent category (nullable for root categories).
        /// </summary>
        public Guid? ParentCategoryId { get; set; }

        /// <summary>
        /// Navigation property to parent category.
        /// </summary>
        public Category? ParentCategory { get; set; }

        /// <summary>
        /// Navigation property to child categories.
        /// </summary>
        public List<Category> Subcategories { get; set; } = new();

        /// <summary>
        /// Gets or sets the collection of product categories associated with the product.
        /// </summary>
        public ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
    }
}

