using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slothsy.Domain.Entities
{
    /// <summary>
    /// Represents an image associated with a product variant.
    /// </summary>
    public class ProductVariantImage
    {
        // Unique identifier for the image
        public Guid Id { get; set; }

        // Foreign key to ProductVariant
        public Guid ProductVariantId { get; set; }

        // Navigation property to ProductVariant
        public ProductVariant ProductVariant { get; set; } = null!;

        // URL of the image
        public string ImageUrl { get; set; } = string.Empty;

        // Ordering number (to control image order in UI)
        public int Order { get; set; }

        // Whether this image is the main (primary) image
        public bool IsMain { get; set; }
    }
}
