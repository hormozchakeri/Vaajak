using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Vaajak.Domain.Common.Auth;
using Vaajak.Domain.Entities;

namespace Vaajak.Application.Services.Auth;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly IConfiguration _configuration;

    public JwtTokenGenerator(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Task<string> GenerateJwtTokenAsync(User user)
    {
        var token = BuildToken(user, expireHours: 1, tokenType: "access");
        return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
    }

    public Task<string> GenerateRefreshTokenAsync(User user)
    {
        var token = BuildToken(user, expireDays: 30, tokenType: "refresh");
        return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
    }

    public string? ValidateRefreshToken(string refreshToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration["JWTConfigs:Key"]!);

        try
        {
            tokenHandler.ValidateToken(refreshToken, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey        = new SymmetricSecurityKey(key),
                ValidateIssuer          = true,
                ValidIssuer             = _configuration["JWTConfigs:Issuer"],
                ValidateAudience        = true,
                ValidAudience           = _configuration["JWTConfigs:Audience"],
                ValidateLifetime        = true,
                ClockSkew               = TimeSpan.Zero,
            }, out var validatedToken);

            var jwt = (JwtSecurityToken)validatedToken;

            var tokenType = jwt.Claims.FirstOrDefault(c => c.Type == "token_type")?.Value;
            if (tokenType != "refresh") return null;

            return jwt.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
        }
        catch
        {
            return null;
        }
    }

    private JwtSecurityToken BuildToken(User user, int expireHours = 0, int expireDays = 0, string tokenType = "access")
    {
        var key   = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTConfigs:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email ?? ""),
            new Claim(ClaimTypes.Name, user.UserName ?? ""),
            new Claim("token_type", tokenType),
        };

        var expires = expireDays > 0
            ? DateTime.UtcNow.AddDays(expireDays)
            : DateTime.UtcNow.AddHours(expireHours > 0 ? expireHours : 1);

        return new JwtSecurityToken(
            issuer: _configuration["JWTConfigs:Issuer"],
            audience: _configuration["JWTConfigs:Audience"],
            claims: claims,
            expires: expires,
            signingCredentials: creds
        );
    }
}
