using AutoMapper;
using Microsoft.Extensions.Logging;
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
    public class ProductVariantService : IProductVariantService
    {
        private readonly IProductVariantRepository _productVariantRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductVariantService> _logger;

        public ProductVariantService(IProductVariantRepository productVariantRepository, IMapper mapper, ILogger<ProductVariantService> logger)
        {
            _productVariantRepository = productVariantRepository ?? throw new ArgumentNullException(nameof(productVariantRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        ///<inheritdoc/>
        public async Task<ProductVariantDto?> GetBySlugAsync(string slug)
        {
            _logger.LogInformation("Retrieving product variant by slug: {Slug}", slug);
            if (string.IsNullOrWhiteSpace(slug))
            {
                _logger.LogWarning("Slug cannot be null or empty.");
                throw new ArgumentException("Slug cannot be null or empty.", nameof(slug));
            }
            var variant = await _productVariantRepository.GetVariantBySlugAsync(slug);
            if (variant == null)
            {
                _logger.LogWarning("No product variant found for slug: {Slug}", slug);
                return null;
            }
           
            return _mapper.Map<ProductVariantDto>(variant);
        }
    }
    
}
