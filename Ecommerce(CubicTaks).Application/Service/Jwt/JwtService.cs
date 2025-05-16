using System;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Ecommerce_CubicTaks_.Application.Service.Jwt;
using Microsoft.IdentityModel.Tokens;

public class JwtService : IJwtService
{
    private readonly string _secret;

    public JwtService()
    {
        _secret = ConfigurationManager.AppSettings["JwtSecretKey"];
    }

    public string GenerateToken(string username, string role, int expireMinutes = 60)
    {
        var key = Encoding.UTF8.GetBytes(_secret);
        var tokenHandler = new JwtSecurityTokenHandler();

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, role)
        };

        var descriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(expireMinutes),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(descriptor);
        return tokenHandler.WriteToken(token);
    }

    public ClaimsPrincipal GetPrincipal(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_secret);

            var validation = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };

            var principal = tokenHandler.ValidateToken(token, validation, out _);
            return principal;
        }
        catch
        {
            return null;
        }
    }
}
