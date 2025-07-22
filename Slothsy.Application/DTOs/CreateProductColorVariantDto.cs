using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slothsy.Application.DTOs
{
    public class CreateProductColorVariantDto
    {
        [Required]
        public Guid ProductId { get; set; }
        [Required]
        public Guid ColorOptionId { get; set; }
        [Required]
        [Range(0.01,double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Discount price must be non-negative.")]
        public decimal? DiscountPrice { get; set; }

        public List<CreateVariantImageDto> Images { get; set; } = new();
    }
}
