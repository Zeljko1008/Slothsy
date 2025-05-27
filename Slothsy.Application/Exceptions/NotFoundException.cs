using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slothsy.Application.Exceptions
{
    public class NotFoundException : ApiException
    {
        public NotFoundException()
            : base("Resource was not found", 404)
        {
        }

        public NotFoundException(string message)
            : base(message, 404)
        {
        }

        public NotFoundException(string message, Exception inner)
            : base(message, 404, inner)
        {
        }
    }
}
