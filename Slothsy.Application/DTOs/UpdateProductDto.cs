using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// Image URL of the product.
        /// </summary>
        public string? ImageUrl { get; set; } 
        /// <summary>
        /// Unique identifier for the category to which the product belongs.
        /// </summary>
        public Guid? CategoryId { get; set; }
    }
}
