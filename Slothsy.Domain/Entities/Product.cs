using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slothsy.Domain.Entities
{
    /// <summary>
    /// Represents a product in the store.
    /// </summary>
    public class Product
    {

        //Unique identifier for the product
        public Guid Id { get; set; }
        //Name of the product
        public string Name { get; set; } = default!;
        //Optional description of the product
        public string Description { get; set; } = default!;
        //Price of the product
        public decimal Price { get; set; }
        //Image URL of the product
        public string ImageUrl { get; set; } = default!;
        //Unique identifier for the category to which the product belongs
        public Guid CategoryId { get; set; }
        //Navigation property for the category
        public Category? Category { get; set; }
    }
}
