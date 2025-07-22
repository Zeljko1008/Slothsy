using Slothsy.Domain.Entities;
using Slothsy.Domain.Enums;
using Slothsy.Domain.Interfaces.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slothsy.Application.Interfaces
{
    public interface IAdminCategoryService
    {
        /// <summary>
        /// Retrieves all categories, with specific gender and age group filters applied.    
        /// </summary>
        /// <param name="gender"></param>
        /// <param name="ageGroup"></param>
        /// <returns></returns>
        Task<List<Category>> GetByGenderAndAgeGroupAsync(Gender gender, AgeGroup ageGroup);

    }
}
