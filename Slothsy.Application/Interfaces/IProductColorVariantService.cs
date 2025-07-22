using Slothsy.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slothsy.Application.Interfaces
{
    public interface IProductColorVariantService
    {

        Task<ProductColorVariantDto?> GetBySlugAsync(string slug);
    }
}
