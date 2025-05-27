using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slothsy.Application.Models
{
    public class ApiErrorResponse
    {

        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public string? Details { get; set; }

        public ApiErrorResponse(int statusCode, string? message = null, string? details = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
            Details = details;
        }

        private static string GetDefaultMessageForStatusCode(int statusCode) =>
           statusCode switch
           {
               400 => "Bad Request",
               401 => "Unauthorized",
               403 => "Forbidden",
               404 => "Resource Not Found",
               409 => "Conflict",
               500 => "Internal Server Error",
               _ => "An unexpected error occurred"
           };

    }
}
