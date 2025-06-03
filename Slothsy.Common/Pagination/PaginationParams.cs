using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slothsy.Common.Pagination
{
    /// <summary>
    /// Represents common pagination parameters used in API queries.
    /// </summary>
    public class PaginationParams
    {
        /// <summary>
        /// Page number to retrieve (1-based index).
        /// </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// Number of items to return per page.
        /// </summary>
        public int PageSize { get; set; } = 50;

        /// <summary>
        /// Maximum allowed page size to prevent excessive data retrieval.
        /// </summary>
        private const int MaxPageSize = 50;

        /// <summary>
        /// Indicates whether to include inactive items in the results.
        /// </summary>
        public bool IncludeInactive { get; set; } = false;

        /// <summary>
        /// Ensures that the page size does not exceed a predefined limit.
        /// </summary>
        public int ValidatedPageSize => PageSize > MaxPageSize ? MaxPageSize : PageSize;

        /// <summary>
        /// Number of items to skip based on current page and page size.
        /// </summary>
        public int Skip => (PageNumber - 1) * ValidatedPageSize;

    }
}
