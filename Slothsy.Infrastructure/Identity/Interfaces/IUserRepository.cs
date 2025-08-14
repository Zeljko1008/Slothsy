using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slothsy.Infrastructure.Identity.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> EmailExistAsync(string email);
    }
}
