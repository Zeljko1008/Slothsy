using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Slothsy.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        [HttpGet("protected")]
        [Authorize] 
        public IActionResult GetProtectedData()
        {
            return Ok(new { message = "Access granted to protected endpoint!" });
        }
    }
}
