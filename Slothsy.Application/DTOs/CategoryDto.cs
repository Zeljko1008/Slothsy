using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slothsy.Application.DTOs
{
    /// <summary>
    /// Data Transfer Object for Category.
    /// </summary>
    public class CategoryDto
    {
        /// <summary>
        /// Unique identifier of the category.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Display name of the category.
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Full description of the category.
        /// </summary>
        public string Description { get; set; } = null!;

        /// <summary>
        /// Optional short description of the category.
        /// </summary>
        public string? ShortDescription { get; set; }

        /// <summary>
        /// Indicates whether the category is active.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Banner image URL shown on homepage or category listing.
        /// </summary>
        public string? BannerImageUrl { get; set; }

        /// <summary>
        /// SEO-friendly URL slug (used in links).
        /// </summary>
        public string? Slug { get; set; }

        /// <summary>
        /// SEO title for search engines.
        /// </summary>
        public string? SeoTitle { get; set; }

        /// <summary>
        /// SEO description for search engines.
        /// </summary>
        public string? SeoDescription { get; set; }

        /// <summary>
        /// Order in which category appears in UI lists.
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Id of the parent category if this is a subcategory.
        /// </summary>
        public Guid? ParentCategoryId { get; set; }

        
    }
}
