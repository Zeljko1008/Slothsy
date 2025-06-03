using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slothsy.Common.Pagination
{
    public class PagedResult<T>
    {
        /// <summary>
        /// The actual items for the current page.
        /// </summary>
        public List<T> Items { get; set; } = new();

        /// <summary>
        /// The total number of items across all pages.
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// The current page number.
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// The size of a single page (number of items).
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// The total number of available pages.
        /// </summary>
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);

        /// <summary>
        /// Whether this is the first page.
        /// </summary>
        public bool HasPreviousPage => PageNumber > 1;

        /// <summary>
        /// Whether there is another page after this one.
        /// </summary>
        public bool HasNextPage => PageNumber < TotalPages;
    }
}
