using ebay.domain.Entities;

namespace ebay.domain.Interfaces;

public interface IRefreshTokenRepository
{
    Task AddRefreshToken(RefreshTokenEntity tokenEntity);
    Task<RefreshTokenEntity?> GetByTokenAndUserId(string token, int userId);

    Task DeleteTokenById(long id);
}
