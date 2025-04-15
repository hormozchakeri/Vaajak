using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vaajak.Domain.Entities;

namespace Vaajak.Domain.Common.Auth
{
    public interface IJwtTokenGenerator
    {
        Task<string> GenerateJwtTokenAsync(User user);
    }
}
