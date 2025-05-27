using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slothsy.Application.Exceptions
{
    public class BadRequestException : ApiException
    {
        public BadRequestException(string message) : base(message, 400) { }
    }

    public class ForbiddenAccessException : ApiException
    {
        public ForbiddenAccessException(string message) : base(message, 403) { }
    }

    public class ConflictException : ApiException
    {
        public ConflictException(string message) : base(message, 409) { }
    }
}
