using Slothsy.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slothsy.Domain.Entities
{
    public class SizeOption
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Display value of the size (e.g. "M", "42", "One Size").
        /// </summary>
        public string Label { get; set; } = string.Empty;

        /// <summary>
        /// Type of size (e.g. clothing, shoes, hats).
        /// </summary>
        public SizeType SizeType { get; set; }

        /// <summary>
        /// Optional ordering number for UI sorting.
        /// </summary>
        public int Order { get; set; }
        /// <summary>
        /// Navigation property: list of product variants that use this size option.dodao sam u size option 
        /// </summary>
        public ICollection<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();

    }
}
