using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slothsy.Domain.Entities
{

    /// <summary>
    /// Represents a user's shopping cart.
    /// </summary>
    public class Cart
    {
        /// <summary>
        /// Primary key.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Identifier for the user associated with the cart.
        /// If anonymous checkout is supported, this can be optional or replaced with session ID.
        /// </summary>
        public string? UserId { get; set; }

        /// <summary>
        /// Timestamp when the cart was last modified.
        /// Used for cart expiration logic.
        /// </summary>
        public DateTime LastModified { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Navigation property for items in the cart.
        /// </summary>
        public ICollection<CartItem> Items { get; set; } = new List<CartItem>();
    }
}
