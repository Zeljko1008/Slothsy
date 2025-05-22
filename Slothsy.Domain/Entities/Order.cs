using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slothsy.Domain.Entities
{
    /// <summary>
    /// Represents a customer's order.
    /// </summary>
    public class Order
    {

        /// <summary>
        /// Primary key.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// User ID of the customer who placed the order.
        /// </summary>
        public string UserId { get; set; } = default!;

        /// <summary>
        /// Date and time when the order was placed.
        /// </summary>
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Total amount of the order.
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Order status (e.g., Pending, Paid, Shipped).
        /// </summary>
        public string Status { get; set; } = "Pending";

        /// <summary>
        /// Foreign key for selected delivery method.
        /// </summary>
        public Guid DeliveryMethodId { get; set; }

        /// <summary>
        /// Navigation property to delivery method.
        /// </summary>
        public DeliveryMethod DeliveryMethod { get; set; } = default!;

        /// <summary>
        /// Navigation property for order items.
        /// </summary>
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    }
}
