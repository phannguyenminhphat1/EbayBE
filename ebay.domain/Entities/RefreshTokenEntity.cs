namespace ebay.domain.Entities;

public class RefreshTokenEntity
{
    public long Id { get; set; }

    public string Token { get; set; } = null!;

    public int UserId { get; set; }

    public DateTime? ExpiresAt { get; set; }

    public DateTime? CreatedAt { get; set; }
    public RefreshTokenEntity(string token, int userId, DateTime createdAt, DateTime expiresAt)
    {
        Token = token;
        UserId = userId;
        CreatedAt = createdAt;
        ExpiresAt = expiresAt;
    }
}