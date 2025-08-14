using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slothsy.Application.DTOs
{
    public class RegisterResponseDto
    {
        /// <summary>
        ///  Gets or sets a value indicating whether the registration was successful.    
        /// </summary>
        public bool Success { get; set; } = false;
        /// <summary>
        /// Gets or sets the title of the response.
        /// </summary>
        public string Title { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the message of the response.
        /// </summary>
        public string Message { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the user ID of the registered user.
        /// </summary>
        public string UserId { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the first name of the registered user.
        /// </summary>
        public string FirstName { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the last name of the registered user.
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
    }
}
