using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slothsy.Application.DTOs
{
    public class CreateVariantImageDto
    {
        [Required]
        public string ImageUrl { get; set; }= null!;
        public int Order { get; set; } = 0;
    }
}
