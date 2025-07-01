using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Slothsy.Application.DTOs;
using Slothsy.Application.Exceptions;
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
    /// <summary>
    /// Service for reading category data.
    /// </summary>
    public class CategoryReadService : ICategoryReadService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryReadService> _logger;

        public CategoryReadService(ICategoryRepository categoryRepository, IMapper mapper, ILogger<CategoryReadService> logger)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        ///<inheritdoc/>
        public async Task<List<CategoryDto>> GetAllCategoriesAsync(bool includeInactive = false)
        {
            _logger.LogInformation("Retrieving all categories with includeInactive={IncludeInactive}", includeInactive);
            var categories = await _categoryRepository.GetAllCategoriesAsync();
            return _mapper.Map<List<CategoryDto>>(categories);

        }
        ///<inheritdoc/>
        public async Task<CategoryDto?> GetCategoryByIdAsync(Guid id, bool includeInactive = false)
        {
            _logger.LogInformation("Retrieving category by ID: {Id} with includeInactive={IncludeInactive}", id, includeInactive);
            var category = await _categoryRepository.GetCategoryByIdAsync(id);
            if (category == null ||(!includeInactive && !category.IsActive) )
            {
                _logger.LogWarning("Category with ID: {Id} not found or inactive", id);
                return null;
            }
            return _mapper.Map<CategoryDto>(category);

        }
        ///<inheritdoc/>
        public async Task<CategoryDto?> GetCategoryBySlugAsync(string slug, bool includeInactive = false)
        {
            _logger.LogInformation("Fetching category with slug: {Slug}", slug);

            var category = await _categoryRepository.GetBySlugAsync(slug, includeInactive);

            if (category == null)
            {
                _logger.LogWarning("Category with slug '{Slug}' not found", slug);
                return null;  // ili možeš baciti NotFoundException, po dogovoru
            }

            var categoryDto = _mapper.Map<CategoryDto>(category);

            return categoryDto;
        }

        ///<inheritdoc/>
        public async Task<List<CategoryDto>> GetMainCategoriesAsync(bool includeInactive = false, string? type = null)
        {
            _logger.LogInformation("Retrieving main categories with includeInactive={IncludeInactive} and type={Type}", includeInactive, type);

            Gender? gender = null;
            AgeGroup? ageGroup = null;

            if (!string.IsNullOrEmpty(type))
            {
                switch (type.ToLower())
                {
                    case "men":
                        gender = Gender.Male;
                        ageGroup = AgeGroup.Adult;
                        break;
                    case "women":
                        gender = Gender.Female;
                        ageGroup = AgeGroup.Adult;
                        break;
                    case "kids":
                        gender = Gender.Unisex;
                        ageGroup = AgeGroup.Child;
                        break;
                    default:
                        throw new ArgumentException($"Invalid type value: {type}");
                }
            }

            var categories = await _categoryRepository.GetMainCategoriesAsync(includeInactive, gender, ageGroup);
            return _mapper.Map <List<CategoryDto>>(categories);
        }
        ///<inheritdoc/>
        public async Task<List<CategoryDto>> GetSubCategoriesAsync(Guid parentCategoryId, bool includeInactive = false)
        {
            _logger.LogInformation("Retrieving subcategories for parent category ID: {ParentCategoryId} with includeInactive={IncludeInactive}", parentCategoryId, includeInactive);
            var subcategories = await _categoryRepository.GetSubcategoriesAsync(parentCategoryId, includeInactive);
            return _mapper.Map<List<CategoryDto>>(subcategories);

        }

        public async Task<List<CategoryDto>> GetSubCategoriesBySlugAsync(string parentCategorySlug, bool includeInactive = false)
        {
           
            var parentCategory = await GetCategoryBySlugAsync(parentCategorySlug, includeInactive);

            if (parentCategory == null)
            {
                _logger.LogWarning("Parent category with slug '{Slug}' not found.", parentCategorySlug);
                throw new NotFoundException($"Parent category with slug '{parentCategorySlug}' was not found.");
            }

           
            return await GetSubCategoriesAsync(parentCategory.Id, includeInactive);
        }
    }
}
