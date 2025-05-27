using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slothsy.Domain.Enums
{
    /// <summary>
    /// Enumeration for the status of an order.
    /// </summary>
    public enum OrderStatus
    {

        /// <summary>Order placed but not yet processed.</summary>
        Pending = 0,

        /// <summary>Order is being prepared or packed.</summary>
        Processing = 1,

        /// <summary>Order has been shipped.</summary>
        Shipped = 2,

        /// <summary>Order delivered to customer.</summary>
        Delivered = 3,

        /// <summary>Order was cancelled.</summary>
        Cancelled = 4
    }
}
