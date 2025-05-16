using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_CubicTaks_.Application.Service.Jwt
{
    public interface IJwtService
    {
        string GenerateToken(string username, string role, int expireMinutes = 60);
        ClaimsPrincipal GetPrincipal(string token);
    }
}
