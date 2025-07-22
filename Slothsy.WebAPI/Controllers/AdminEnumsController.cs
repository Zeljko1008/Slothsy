using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Slothsy.Application.DTOs;
using Slothsy.Application.Interfaces;
using Slothsy.Common.Helpers;
using Slothsy.Domain.Enums;

namespace Slothsy.WebAPI.Controllers
{
    [ApiController]
    [Route("api/admin/enums")]
    public class AdminEnumsController : ControllerBase
    {
        private readonly IAdminEnumService _enumService;

        public AdminEnumsController(IAdminEnumService enumService)
        {
            _enumService = enumService ?? throw new ArgumentNullException(nameof(enumService));
        }
        [HttpGet]
        public IActionResult GetProductFormData()
        {
            var result = _enumService.GetAllProductFormEnums();
            return Ok(result);
        }
    }
}

