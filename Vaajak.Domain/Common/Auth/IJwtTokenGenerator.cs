using Vaajak.Domain.Entities;

namespace Vaajak.Domain.Common.Auth;

public interface IJwtTokenGenerator
{
    Task<string> GenerateJwtTokenAsync(User user);
    Task<string> GenerateRefreshTokenAsync(User user);
    string? ValidateRefreshToken(string refreshToken);
}
