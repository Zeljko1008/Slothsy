using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slothsy.Domain.Entities
{
    /// <summary>
    /// Represents a shipping/delivery option for an order.
    /// </summary>
    public class DeliveryMethod
    {

        /// <summary>
        /// Primary key.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The name of the delivery method (e.g., "Standard Shipping", "Express").
        /// </summary>
        public string Name { get; set; } = default!;

        /// <summary>
        /// Estimated delivery time (e.g., "2-3 business days").
        /// </summary>
        public string? DeliveryTime { get; set; }

        /// <summary>
        /// Additional description (e.g., "Delivery via DHL").
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Price of this delivery method.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Navigation property for orders using this delivery method.
        /// </summary>
        public ICollection<Order> Orders { get; set; } = new List<Order>();

    }
}
