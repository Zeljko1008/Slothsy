using Slothsy.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slothsy.Application.DTOs
{
    public class CategoryForFormDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Gender Gender { get; set; }
        public AgeGroup AgeGroup { get; set; }
    }
}
