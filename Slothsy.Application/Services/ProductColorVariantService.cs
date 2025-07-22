using AutoMapper;
using Slothsy.Application.DTOs;
using Slothsy.Application.Interfaces;
using Slothsy.Domain.Interfaces.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slothsy.Application.Services
{
    public class ProductColorVariantService : IProductColorVariantService
    {
        private readonly IProductColorVariantRepository _productColorVariantRepository;
        private readonly IMapper _mapper;

        public ProductColorVariantService(IProductColorVariantRepository productColorVariantRepository, IMapper mapper)
        {
            _productColorVariantRepository = productColorVariantRepository;
            _mapper = mapper;
        }
        ///<inheritdoc/>
        public async Task<ProductColorVariantDto?> GetBySlugAsync(string slug)
        {
            var entity = await _productColorVariantRepository.GetBySlugAsync(slug);
            return entity == null ? null : _mapper.Map<ProductColorVariantDto>(entity);
        }
    }
}
