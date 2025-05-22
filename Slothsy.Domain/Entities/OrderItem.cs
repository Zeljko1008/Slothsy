using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slothsy.Domain.Entities
{
    /// <summary>
    /// Represents a product item in a customer's order.
    /// </summary>
    public class OrderItem
    {

        /// <summary>
        /// Primary key.
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Foreign key to the Order.
        /// </summary>
        public Guid OrderId { get; set; }
        /// <summary>
        /// Foreign key to the Product.
        /// </summary>
        public Guid ProductId { get; set; }
        /// <summary>
        /// Name of the product.
        /// </summary>
        public string ProductName { get; set; } = default!;
        /// <summary>
        /// Price of the product at the time of order.
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Quantity of the product selected.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Image URL of the product item.
        /// </summary>
        public string ImageUrl { get; set; } = default!;
        /// <summary>
        /// Navigation property to Order.
        /// </summary>
        public Order Order { get; set; } = default!;

    }
}
