using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slothsy.Domain.Entities
{
    /// <summary>
    /// Represents a product category, which can have subcategories (e.g. "Electronics" > "Mobile Phones").
    /// </summary>


    public class Category
    {
        /// <summary>
        /// Unique identifier for the category.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Name of the category.
        /// </summary>
        public string Name { get; set; } = default!;

        /// <summary>
        /// Optional description of the category.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Short description or summary of the category, used for display purposes.
        /// </summary>
        public string? ShortDescription { get; set; }

        /// <summary>
        /// Indicates whether the category is currently active and available for use.
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// URL or path to the category banner image.
        /// </summary>
        public string? BannerImageUrl { get; set; }

        /// <summary>
        /// Display order of the category in lists or menus.
        /// </summary>
        public int Order { get; set; } = 0;

        /// <summary>
        /// SEO title for the category, used in page metadata.
        /// </summary>
        public string? SeoTitle { get; set; }

        /// <summary>
        /// SEO keywords for the category, used in page metadata.
        /// </summary>
        public string? SeoDescription { get; set; }

        /// <summary>
        /// SEO slug for the category, used in URLs to make them more readable and SEO-friendly.
        /// </summary>
        public string? Slug { get; set; }

        /// <summary>
        /// Navigation property for the parent category, if this is a subcategory.
        /// </summary>
        public Guid? ParentCategoryId { get; set; }

        /// <summary>
        /// Reference to the parent category entity.
        /// </summary>
        public Category? ParentCategory { get; set; }

        /// <summary>
        /// Collection of subcategories that belong to this category.
        /// </summary>
        public ICollection<Category>? Subcategories { get; set; }

        /// <summary>
        /// Collection of products assigned to this category.
        /// </summary>
        public ICollection<Product>? Products { get; set; }
    }
}

