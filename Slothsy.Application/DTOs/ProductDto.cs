using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slothsy.Application.DTOs
{
    /// <summary>
    /// Data Transfer Object (DTO) for a product.
    /// </summary>
    public class ProductDto
    {
        /// <summary>
        /// Unique identifier for the product.
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Name of the product.
        /// </summary>
        public string Name { get; set; } = null!;
        /// <summary>
        /// Optional description of the product.
        /// </summary>
        public string Description { get; set; } = null!;
        /// <summary>
        /// Price of the product.
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Image URL of the product.
        /// </summary>
        public string ImageUrl { get; set; } = null!;
        /// <summary>
        /// Unique identifier for the category to which the product belongs.
        /// </summary>
        public Guid CategoryId { get; set; }
        /// <summary>
        /// Category name of the product.
        /// </summary>
        public string CategoryName { get; set; } = null!;
    }
}
