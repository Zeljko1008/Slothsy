using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slothsy.Domain.Entities
{
    /// <summary>
    /// Represents a product item in a user's cart.
    /// </summary>
    public class CartItem
    {
        /// <summary>
        /// Primary key.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Foreign key to the Cart.
        /// </summary>
        public Guid CartId { get; set; }

        /// <summary>
        /// Foreign key to the Product.
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Quantity of the product selected.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Navigation property to Cart.
        /// </summary>
        public Cart? Cart { get; set; }

        /// <summary>
        /// Navigation property to Product.
        /// </summary>
        public Product? Product { get; set; }
    }
}
