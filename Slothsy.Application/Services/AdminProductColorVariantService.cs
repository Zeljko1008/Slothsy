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
    public class AdminProductColorVariantService : IAdminProductColorVariantService
    {
        private readonly IProductColorVariantRepository _productColorVariantRepository;
        private readonly IColorOptionRepository _colorOptionRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public AdminProductColorVariantService(IProductColorVariantRepository productColorVariantRepository, IColorOptionRepository colorOptionRepository, IProductRepository productRepository, IMapper mapper)
        {
            _productColorVariantRepository = productColorVariantRepository ?? throw new ArgumentNullException(nameof(productColorVariantRepository));
            _colorOptionRepository = colorOptionRepository ?? throw new ArgumentNullException(nameof(colorOptionRepository));
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));


            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Guid> AddColorVariantAsync( CreateProductColorVariantDto dto)
        {
            var product = await _productRepository.GetByIdAsync(dto.ProductId, true);
            if (product == null)
                throw new Exception("Product not found.");

            var colorOption = await _colorOptionRepository.GetByIdAsync(dto.ColorOptionId);
            if (colorOption == null)
                throw new Exception("Color option not found.");

            var slug = $"{product.Slug}-{Slugify(colorOption.Name)}";

            var colorVariant= _mapper.Map<ProductColorVariant>(dto);

            colorVariant.Id = Guid.NewGuid();

            colorVariant.Slug = slug;
            colorVariant.ProductId = product.Id;
            colorVariant.ColorOptionId = colorOption.Id;

            if (colorVariant.Images != null)
            {
                foreach (var img in colorVariant.Images)
                {
                    img.ProductColorVariantId = colorVariant.Id;
                }
            }
            if (colorVariant.Variants != null)
            {
                foreach (var variant in colorVariant.Variants)
                {
                    variant.ProductColorVariantId = colorVariant.Id;
                }
            }

            await _productColorVariantRepository.AddAsync(colorVariant);

            await _productColorVariantRepository.SaveChangesAsync();

            return colorVariant.Id;
        }
        private string Slugify(string input)
        {
            return input
                .ToLowerInvariant()
                .Trim()
                .Replace(" ", "-")
                .Replace(",", "")
                .Replace(".", "")
                .Replace("'", "")
                .Replace("\"", "")
                .Replace("č", "c").Replace("ć", "c")
                .Replace("š", "s").Replace("đ", "dj").Replace("ž", "z");
        }

        public async Task<ProductColorVariantDto?> GetColorVariantByIdAsync(Guid colorVariantId)
        {
            var colorVariant = await _productColorVariantRepository.GetByIdWithIncludesAsync(colorVariantId);
            if (colorVariant == null)
                return null;
            var dto = _mapper.Map<ProductColorVariantDto>(colorVariant);
            return dto;
        }

        public async Task<IEnumerable<ProductColorVariantDto?>> GetAllAsync(Guid productId)
        {
            var productColorVariants = await _productColorVariantRepository.GetAllAsync(productId);

            if (productColorVariants == null || !productColorVariants.Any())
                return Enumerable.Empty<ProductColorVariantDto>();

            var dtos = _mapper.Map<IEnumerable<ProductColorVariantDto>>(productColorVariants);

            return dtos;


        }
    }
}
