using Slothsy.Application.DTOs;
using Slothsy.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slothsy.Application.Interfaces
{
    public interface IAdminSizeOptionService
    {
        /// <summary>
        ///  Adds a new size option asynchronously.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<SizeOptionDto>> GetAllAsync();

        /// <summary>
        /// Retrieves a collection of size options that match the specified size type.
        /// </summary>
        /// <param name="sizeType">The type of size to filter the options by. Must be a valid <see cref="SizeType"/> value.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains an <see cref="IEnumerable{T}"/>
        /// of <see cref="SizeOptionDto"/> objects that correspond to the specified size type. If no matching size
        /// options are found, the result will be an empty collection.</returns>
        Task<IEnumerable<SizeOptionDto>> GetBySizeTypeAsync(SizeType sizeType);
    }
}
