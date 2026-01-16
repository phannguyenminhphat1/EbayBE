using System.Security.Claims;
using ebay.domain.Entities;

namespace ebay.domain.Interfaces;

public interface IJwtService
{
    string GenerateToken(UserEntity user);
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);

    string DecodeToken(string token);
    string GenerateRefreshToken();
}