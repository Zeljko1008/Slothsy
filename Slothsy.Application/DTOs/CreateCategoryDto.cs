using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slothsy.Application.DTOs
{
    public class CreateCategoryDto
    {

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
        /// SEO-friendly URL slug (used in links).
        /// </summary>
        public string? Slug { get; set; }

        /// <summary>
        /// Unique identifier for the parent category, if this category is a subcategory.
        /// </summary>
        public Guid? ParentCategoryId { get; set; }
    }
}
