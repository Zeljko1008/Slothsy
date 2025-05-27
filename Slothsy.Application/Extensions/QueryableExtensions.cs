using Slothsy.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slothsy.Application.Extensions
{
    /// <summary>
    /// Provides extension methods for paginating queryable data.
    /// </summary>
    public static class QueryableExtensions
    {
        /// <summary>
        /// Applies pagination to the queryable source and returns a paginated result.
        /// </summary>
        /// <typeparam name="T">The type of the elements.</typeparam>
        /// <param name="query">The source queryable.</param>
        /// <param name="paginationParams">Pagination parameters.</param>
        /// <returns>A paginated result containing the items and metadata.</returns>
        public static async Task<PagedResult<T>> ToPagedResultAsync<T>(this IQueryable<T> query, PaginationParams paginationParams, CancellationToken cancellationToken = default)
        {
            var totalCount = await Task.Run(() => query.Count(), cancellationToken);

            var items = await Task.Run(() =>
                query
                    .Skip((paginationParams.PageNumber - 1) * paginationParams.ValidatedPageSize)
                    .Take(paginationParams.ValidatedPageSize)
                    .ToList(), cancellationToken);

            return new PagedResult<T>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = paginationParams.PageNumber,
                PageSize = paginationParams.ValidatedPageSize
            };
        }
    }
}
