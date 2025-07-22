using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slothsy.Application.DTOs
{
    public class CreateProductVariantDto
    {
        public Guid ProductColorVariantId { get; set; }
        public Guid SizeOptionId { get; set; }
        public int StockQuantity { get; set; }
    }
}
