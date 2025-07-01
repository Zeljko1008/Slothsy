using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slothsy.Domain.Entities
{
    public class ColorOption
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Name of the color (e.g., "Red", "Navy Blue").
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Optional hex color code or any representation of the color.
        /// </summary>
        public string? HexCode { get; set; }

        /// <summary>
        /// Optional image URL for color swatch or pattern.
        /// </summary>
        public string? ImageUrl { get; set; }

        /// <summary>
        /// Order for UI sorting or display.
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Navigation property: list of product variants with this color.
        /// </summary>
        public ICollection<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();
    }
}
