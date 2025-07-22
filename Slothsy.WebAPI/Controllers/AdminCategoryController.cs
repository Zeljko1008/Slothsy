using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Slothsy.Application.DTOs;
using Slothsy.Application.Interfaces;
using Slothsy.Domain.Enums;

namespace Slothsy.WebAPI.Controllers
{
    [Route("api/admin/categories")]
    [ApiController]
    public class AdminCategoryController : ControllerBase
    {

        private readonly IAdminCategoryService _adminCategoryService;
        private readonly IMapper _mapper;

        public AdminCategoryController(IAdminCategoryService adminCategoryService, IMapper mapper)
        {
            _adminCategoryService = adminCategoryService;
            _mapper = mapper;
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetByGenderAndAgeGroup([FromQuery] int gender, [FromQuery] int ageGroup)
        {
            var genderEnum = (Gender)gender;
            var ageGroupEnum = (AgeGroup)ageGroup;

            var categories = await _adminCategoryService.GetByGenderAndAgeGroupAsync(genderEnum, ageGroupEnum);
            var result = _mapper.Map<List<CategoryForFormDto>>(categories);
            return Ok(result);


        }
    }
}
