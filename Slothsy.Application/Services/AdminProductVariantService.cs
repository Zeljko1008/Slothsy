using AutoMapper;
using Slothsy.Application.DTOs;
using Slothsy.Application.Interfaces;
using Slothsy.Domain.Entities;
using Slothsy.Domain.Interfaces.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slothsy.Application.Services
{
    public class AdminProductVariantService : IAdminProductVariantService
    {
        private readonly IProductVariantRepository _productVariantRepository;
        private readonly IProductColorVariantRepository _productColorVariantRepository;
        private readonly ISizeOptionRepository _sizeOptionRepository;
        private readonly IMapper _mapper;

        public AdminProductVariantService(
            IProductVariantRepository productVariantRepository,
            IProductColorVariantRepository productColorVariantRepository,
            ISizeOptionRepository sizeOptionRepository,
            IMapper mapper)
        {
            _productVariantRepository = productVariantRepository;
            _productColorVariantRepository = productColorVariantRepository;
            _sizeOptionRepository = sizeOptionRepository;
            _mapper = mapper;
        }

        ///<inheritdoc />
        public async Task<Guid> AddProductVariantAsync(CreateProductVariantDto dto)
        {
            var colorVariant = await _productColorVariantRepository.GetByIdWithIncludesAsync(dto.ProductColorVariantId);
            if(colorVariant == null)
            {
                throw new Exception("Product color variant not found.");
            }
            var sizeOption = await _sizeOptionRepository.GetByIdAsync(dto.SizeOptionId);
            if(sizeOption == null)
            {
                throw new Exception("Size option not found.");
            }
            var slug = $"{colorVariant.Slug}-{Slugify(sizeOption.Label)}".ToLower();

            var variant = _mapper.Map<ProductVariant>(dto);
            variant.Id = Guid.NewGuid(); 
            variant.Slug = slug;

            await _productVariantRepository.AddAsync(variant);
            await _productVariantRepository.SaveChangesAsync();

            return variant.Id;

        }
        /// <summary>
        /// Converts the specified input string into a URL-friendly "slug" format.
        /// </summary>
        /// <remarks>The method performs the following transformations: <list type="bullet">
        /// <item><description>Converts the string to lowercase using invariant culture.</description></item>
        /// <item><description>Trims leading and trailing whitespace.</description></item> <item><description>Replaces
        /// spaces and certain special characters (e.g., "/", ".", "'") with hyphens or other
        /// substitutes.</description></item> <item><description>Normalizes specific accented characters (e.g., "č" to
        /// "c", "š" to "s").</description></item> </list> This method is useful for generating URL slugs or other
        /// identifiers that require a clean, consistent format.</remarks>
        /// <param name="input">The input string to be converted. Must not be null.</param>
        /// <returns>A string formatted as a slug, with characters normalized, spaces replaced by hyphens, and special characters
        /// removed or substituted.</returns>
        private string Slugify(string input)
        {
            return input
                .ToLowerInvariant()
                .Trim()
                .Replace(" ", "-")
                .Replace("/", "-")   
                .Replace(",", "")
                .Replace(".", "_")   
                .Replace("'", "")
                .Replace("\"", "")
                .Replace("č", "c").Replace("ć", "c")
                .Replace("š", "s").Replace("đ", "dj").Replace("ž", "z");
        }

        ///<inheritdoc />
        public async Task<ProductVariantDto?> GetProductVariantByIdAsync(Guid id)
        {
            var variant = await _productVariantRepository.GetByIdAsync(id);
            if (variant == null)
            {
                return null;
            }
            var productVariantDto = _mapper.Map<ProductVariantDto>(variant);
            return productVariantDto;


        }

        public async Task<IEnumerable<ProductVariantDto>> GettAllAsync(Guid productColorVariantId)
        {
            var productVariant = await _productVariantRepository.GettAllAsync(productColorVariantId);

            if (productVariant == null || !productVariant.Any())
            {
                return Enumerable.Empty<ProductVariantDto>();
            }

            var productVariantDtos = _mapper.Map<IEnumerable<ProductVariantDto>>(productVariant);

            return productVariantDtos;
        }
    }
}
