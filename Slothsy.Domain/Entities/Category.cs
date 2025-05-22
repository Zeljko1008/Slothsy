using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slothsy.Domain.Entities
{
    /// <summary>
    /// Represents a product category (e.g.,"Clothing", "Electronics").
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
        /// Navigation property for the products in this category.
        /// </summary>
        public ICollection<Product>? Products { get; set; }
    }
}
