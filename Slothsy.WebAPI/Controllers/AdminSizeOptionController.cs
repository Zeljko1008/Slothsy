using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Slothsy.Application.DTOs;
using Slothsy.Application.Interfaces;
using Slothsy.Domain.Enums;

namespace Slothsy.WebAPI.Controllers
{
    [Route("api/admin/size-options")]
    [ApiController]
    public class AdminSizeOptionController : ControllerBase
    {
         private readonly IAdminSizeOptionService _adminSizeOptionService;

        public AdminSizeOptionController(IAdminSizeOptionService adminSizeOptionService)
            {
            _adminSizeOptionService = adminSizeOptionService ?? throw new ArgumentNullException(nameof(adminSizeOptionService));
        }

        // GET: api/admin/size-options
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SizeOptionDto>>> GetAll()
        {
            var result = await _adminSizeOptionService.GetAllAsync();
            return Ok(result);
        }

        // GET: api/admin/size-options/{id}
        //[HttpGet("{id}")]
        //public async Task<ActionResult<SizeOptionDto>> GetById(Guid id)
        //{
        //    var option = await _adminSizeOptionService.GetByIdAsync(id);
        //    if (option == null)
        //        return NotFound();

        //    return Ok(option);
        //}


        /// <summary>
        /// Gets size options by their type.    
        /// </summary>
        /// <param name="sizeType"></param>
        /// <returns></returns>
        [HttpGet("by-type")]
        public async Task<ActionResult<IEnumerable<SizeOptionDto>>> GetBySizeType([FromQuery] SizeType sizeType)
        {
            var result = await _adminSizeOptionService.GetBySizeTypeAsync(sizeType);
            return Ok(result);
        }
    }
}
