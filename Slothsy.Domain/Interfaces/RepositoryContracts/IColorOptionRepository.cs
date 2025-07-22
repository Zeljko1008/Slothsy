using Slothsy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slothsy.Domain.Interfaces.RepositoryContracts
{
    public interface IColorOptionRepository
    {
        /// <summary>
        /// Retrieves a <see cref="ColorOption"/> object by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the <see cref="ColorOption"/> to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the  <see cref="ColorOption"/>
        /// object with the specified identifier, or <see langword="null"/>  if no matching object is found.</returns>
        Task<ColorOption> GetByIdAsync(Guid id);
    }
}
