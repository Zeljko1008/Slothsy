using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slothsy.Domain.ValueObjects
{
    /// <summary>
    /// Represents a shipping address used in an order.
    /// </summary>
    public class Address
    {
        /// <summary>
        /// First name of the recipient.
        /// </summary>
        public string FirstName { get; set; } = default!;
        /// <summary>
        /// Last name of the recipient.
        /// </summary>
        public string LastName { get; set; } = default!;
        /// <summary>
        /// Street name.
        /// </summary>
        public string Street { get; set; } = default!;
        /// <summary>
        /// City name.
        /// </summary>
        public string City { get; set; } = default!;
        
        /// <summary>
        /// Postal code.
        /// </summary>
        public string ZipCode { get; set; } = default!;
        /// <summary>
        /// Country name.
        /// </summary>
        public string Country { get; set; } = default!;
    }
}
