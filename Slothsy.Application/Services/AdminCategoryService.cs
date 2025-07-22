using Slothsy.Application.Interfaces;
using Slothsy.Domain.Entities;
using Slothsy.Domain.Enums;
using Slothsy.Domain.Interfaces.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slothsy.Application.Services
{
    public class AdminCategoryService : IAdminCategoryService
    {

        private readonly ICategoryRepository _categoryRepository;

        public AdminCategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<List<Category>> GetByGenderAndAgeGroupAsync(Gender gender, AgeGroup ageGroup)
        {
            return await _categoryRepository.GetByGenderAndAgeGroupAsync(gender, ageGroup);
        }
    }
}
