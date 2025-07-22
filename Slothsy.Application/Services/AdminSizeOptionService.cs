using AutoMapper;
using Slothsy.Application.DTOs;
using Slothsy.Application.Interfaces;
using Slothsy.Domain.Enums;
using Slothsy.Domain.Interfaces.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slothsy.Application.Services
{
    public class AdminSizeOptionService:IAdminSizeOptionService
    {
        private readonly ISizeOptionRepository _sizeOptionRepository;
        private readonly IMapper _mapper;

        public AdminSizeOptionService(ISizeOptionRepository sizeOptionRepository, IMapper mapper)
        {
            _sizeOptionRepository = sizeOptionRepository ?? throw new ArgumentNullException(nameof(sizeOptionRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<SizeOptionDto>> GetAllAsync()
        {
           var sizeOption= await _sizeOptionRepository.GetAllAsync();

              if(sizeOption == null || !sizeOption.Any())
              {
                return Enumerable.Empty<SizeOptionDto>();
            }
              return _mapper.Map<IEnumerable<SizeOptionDto>>(sizeOption);
        }

        public async Task<IEnumerable<SizeOptionDto>> GetBySizeTypeAsync(SizeType sizeType)
        {
           var sizeOptions= await _sizeOptionRepository.GetBySizeTypeAsync(sizeType);
            
            return _mapper.Map<IEnumerable<SizeOptionDto>>(sizeOptions);
        }
    }
}
