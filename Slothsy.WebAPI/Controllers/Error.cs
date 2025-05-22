using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Slothsy.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Error : ControllerBase
    {
        [HttpGet("")]
        public IActionResult HandleError()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context?.Error;

            if (exception == null)
            {
                return Problem(
                    detail: "An unknown error occurred.",
                    title: "Unknown Error",
                    statusCode: 500
                );
            }

            // Switch to handle different exception types
            switch (exception)
            {
                case ArgumentException argEx:
                    return Problem(
                        detail: argEx.Message,
                        title: "Invalid Argument",
                        statusCode: 400 // Bad Request
                    );

                case InvalidOperationException invOpEx:
                    return Problem(
                        detail: invOpEx.Message,
                        title: "Invalid Operation",
                        statusCode: 400 // Bad Request
                    );



                case UnauthorizedAccessException unauthorizedEx:
                    return Problem(
                        detail: unauthorizedEx.Message,
                        title: "Unauthorized Access",
                        statusCode: 401 // Unauthorized
                    );

                case Exception ex:
                    // Generic exception handler
                    return Problem(
                        detail: ex.Message,
                        title: "Internal Server Error",
                        statusCode: 500 // Internal Server Error
                    );
                //In this case, we are returning a generic 500 error for any other exceptions that are not specifically handled.
                default:
                    return Problem(
                        detail: exception.Message,
                        title: "Internal Server Error",
                        statusCode: 500 // Internal Server Error
                        );
            }
        }
    }
}
