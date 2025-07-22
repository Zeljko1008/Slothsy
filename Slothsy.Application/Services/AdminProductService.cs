using AutoMapper;
using Slothsy.Application.DTOs;
using Slothsy.Application.Interfaces;
using Slothsy.Common.Pagination;
using Slothsy.Domain.Entities;
using Slothsy.Domain.Interfaces.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Slothsy.Application.Services
{
    public class AdminProductService : IAdminProductService
    {
       private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public AdminProductService( IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _mapper = mapper;
        }

        public async Task<PagedResult<ProductDto>> GetAllAsync(PaginationParams paginationParams)
        {
            var paginatedResult = await _productRepository.GetAllAsync(paginationParams);
            var dtoItems = _mapper.Map<List<ProductDto>>(paginatedResult.Items);

            return new PagedResult<ProductDto>
            {
                Items = dtoItems,
                TotalCount = paginatedResult.TotalCount,
                PageNumber = paginatedResult.PageNumber,
                PageSize = paginatedResult.PageSize
            };
        }
        private async Task<string> GenerateUniqueSlugAsync(string name)
        {
            var baseSlug = GenerateSlug(name);
            var slug = baseSlug;
            int i = 1;

            while (await SlugExistsAsync(slug))
            {
                slug = $"{baseSlug}-{i}";
                i++;
            }

            return slug;
        }

        private string GenerateSlug(string phrase)
        {
            string str = phrase.ToLowerInvariant();

            //remove accents and diacritics
            str = str.Normalize(NormalizationForm.FormD);

            var stringBuilder = new StringBuilder();

            foreach (var c in str)
            {
                // remove diacritical marks
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }
            str = stringBuilder.ToString().Normalize(NormalizationForm.FormC);

            // remove all non-alphanumeric characters except spaces and dashes
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");

            // replace spaces and multiple dashes with a single dash
            str = Regex.Replace(str, @"\s+", "-").Trim('-');

            return str;
        }



        public async Task<bool> SlugExistsAsync(string slug)
        {
            return await _productRepository.ExistsBySlugAsync(slug);
        }


        public async Task<Guid> AddProductAsync(CreateProductDto createProductDto)
        {
            var product = _mapper.Map<Product>(createProductDto);
            product.Slug =await GenerateUniqueSlugAsync(product.Name);

            if (createProductDto.CategoryIds.Any())
            {
                foreach (var categoryId in createProductDto.CategoryIds)

                {
                    product.ProductCategories.Add(new ProductCategory
                    {
                        ProductId = product.Id,
                        CategoryId = categoryId
                    });
                }
            }


            await _productRepository.AddAsync(product);

            await _productRepository.SaveChangesAsync();

            return product.Id;
        }

        public async Task<ProductDto?> GetProductById(Guid id,bool includeInactive = false)
        {
            var product =await  _productRepository.GetByIdAsync(id, includeInactive);

            var dto = _mapper.Map<ProductDto>(product);
            return dto;

        }
    }
}
