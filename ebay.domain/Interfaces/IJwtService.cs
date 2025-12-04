using ebay.domain.Entities;

namespace ebay.domain.Interfaces;

public interface IJwtService
{
    string GenerateToken(UserEntity user);
    bool ValidateToken(string token);
    string DecodeToken(string token);
    string GenerateRefreshToken();
}