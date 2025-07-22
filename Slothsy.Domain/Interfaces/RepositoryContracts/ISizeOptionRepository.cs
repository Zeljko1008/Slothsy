using Slothsy.Domain.Entities;
using Slothsy.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slothsy.Domain.Interfaces.RepositoryContracts
{
    public interface ISizeOptionRepository
    {
        /// <summary>
        /// Retrieves a size option by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the size option.</param>
        /// <returns>A <see cref="SizeOption"/> representing the size option if found; otherwise, <see langword="null"/>.</returns>
        Task<SizeOption?> GetByIdAsync(Guid id);
        /// <summary>
        /// Retrieves all size options.
        /// </summary>
        /// <returns>A list of all size options.</returns>
        Task<IEnumerable<SizeOption>> GetAllAsync();
        
        /// <summary>
        /// Adds a new size option to the repository.
        /// </summary>
        /// <param name="sizeOption">The size option to add.</param>
        Task AddAsync(SizeOption sizeOption);
        
        /// <summary>
        /// Updates an existing size option in the repository.
        /// </summary>
        /// <param name="sizeOption">The size option to update.</param>
        Task UpdateAsync(SizeOption sizeOption);
        
        /// <summary>
        /// Deletes a size option from the repository.
        /// </summary>
        /// <param name="id">The unique identifier of the size option to delete.</param>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// Retrieves a collection of size options that match the specified size type.
        /// </summary>
        /// <param name="sizeType">The type of size to filter the options by. Must be a valid <see cref="SizeType"/> value.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of 
        /// <see cref="SizeOption"/> objects that correspond to the specified size type. If no matching options are
        /// found,  the collection will be empty.</returns>
        Task<IEnumerable<SizeOption>> GetBySizeTypeAsync(SizeType sizeType);
    }
}
