namespace ebay.domain.Entities;

public class UserEntity
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? FullName { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? Deleted { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    private readonly List<UserRoleEntity> _UserRoles = new();
    public IReadOnlyCollection<UserRoleEntity> UserRoleEntity => _UserRoles;

    private readonly List<RefreshTokenEntity> _RefreshTokens = new();
    public IReadOnlyCollection<RefreshTokenEntity> RefreshTokens => _RefreshTokens;

    private UserEntity() { }
    public UserEntity(string username, string passwordHash, string fullName, string email, DateTime createdAt, bool? deleted)
    {
        Username = username;
        PasswordHash = passwordHash;
        FullName = fullName;
        Email = email;
        CreatedAt = createdAt;
        Deleted = deleted;
    }

    public void AddRole(UserRoleEntity role)
    {
        _UserRoles.Add(role);
    }

}