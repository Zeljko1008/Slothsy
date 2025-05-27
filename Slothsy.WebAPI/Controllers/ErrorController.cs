using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Slothsy.Application.Exceptions;
using Slothsy.Application.Models;

namespace Slothsy.WebAPI.Controllers
{
    [ApiController]
    [Route("error")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<ErrorController> _logger;
        private readonly ProblemDetailsFactory _problemDetailsFactory;

        public ErrorController(
            IWebHostEnvironment env,
            ILogger<ErrorController> logger,
            ProblemDetailsFactory problemDetailsFactory)
        {
            _env = env;
            _logger = logger;
            _problemDetailsFactory = problemDetailsFactory;
        }

        [Route("")]
        [AcceptVerbs("GET", "POST", "PUT", "DELETE", "PATCH")]
        public IActionResult HandleError()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context?.Error;

            var statusCode = exception switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                ForbiddenAccessException => StatusCodes.Status403Forbidden,
                BadRequestException => StatusCodes.Status400BadRequest,
                ConflictException => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status500InternalServerError
            };

            var title = _env.IsDevelopment()
                ? exception?.Message ?? "Unexpected error"
                : GetGenericTitle(statusCode);

            var detail = _env.IsDevelopment() ? exception?.ToString() : null;

            var problem = _problemDetailsFactory.CreateProblemDetails(
                HttpContext,
                statusCode,
                title: title,
                detail: detail,
                instance: HttpContext.Request.Path
            );

            _logger.LogError(exception, "Unhandled exception occurred");

            return StatusCode(statusCode, problem);
        }

        private static string GetGenericTitle(int statusCode) => statusCode switch
        {
            400 => "Bad Request",
            401 => "Unauthorized",
            403 => "Forbidden",
            404 => "Resource Not Found",
            409 => "Conflict Occurred",
            _ => "An unexpected error occurred"
        };
    }
}
