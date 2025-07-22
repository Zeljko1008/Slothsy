using Slothsy.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Slothsy.Application.DTOs
{
    /// <summary>
    /// Data Transfer Object (DTO) for creating a new product.
    /// </summary>
    public class CreateProductDto
    {
        /// <summary>
        /// Name of the product.
        /// </summary>
        [Required]
        public string Name { get; set; } = null!;
        /// <summary>
        /// Gets or sets a brief description of the item.
        /// </summary>
        public string? ShortDescription { get; set; }

        /// <summary>
        /// Optional description of the product.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the brand name of the product.
        /// </summary>
        [Required]
        public string Brand { get; set; } = null!;

        /// <summary>
        /// Gets or sets the purpose associated with the current instance.
        /// </summary>
        [Required] 
        public ProductPurpose Purpose { get; set; } 

        /// <summary>
        /// Gets or sets the fit description for the item.
        /// </summary>
        [Required] 
        public FitType Fit { get; set; }

        /// <summary>
        /// Gets or sets the material associated with the object.
        /// </summary>
        [Required] 
        public MaterialType Material { get; set; }

        /// <summary>
        /// Gets or sets the current season associated with the object.
        /// </summary>
        [Required] 
        public Season Season { get; set; }

        /// <summary>
        /// Gets or sets the gender associated with the entity.
        /// </summary>
        [Required] 
        public Gender Gender { get; set; }

        /// <summary>
        /// Gets or sets the age group classification for an individual or entity.
        /// </summary>
        [Required] 
        public AgeGroup AgeGroup { get; set; } 

        /// <summary>
        /// Gets or sets the list of category identifiers.
        /// </summary>
        [Required] 
        public List<Guid> CategoryIds { get; set; } = new();

        /// <summary>
        /// SEO title for the product, used for search engine optimization.
        /// </summary>
        public string? SeoTitle { get; set; }

        /// <summary>
        /// SEO description for the product, used for search engine optimization.
        /// </summary>
        public string? SeoDescription { get; set; }

        
    }
}
