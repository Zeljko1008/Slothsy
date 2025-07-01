using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slothsy.Application.DTOs
{
    public class ProductCategoryDto
    {
        public Guid ProductId { get; set; }
        public Guid CategoryId { get; set; }

        public string ProductName { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
    }
}
