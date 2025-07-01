using Slothsy.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slothsy.Application.DTOs
{
    public class SizeOptionDto
    {
        public Guid Id { get; set; }

        public string Label { get; set; } = string.Empty;

        public SizeType SizeType { get; set; }

        public int Order { get; set; }
    }
}
