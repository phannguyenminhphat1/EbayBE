namespace ebay.domain.Entities;

public class UserEntity
{
    public int Id { get; private set; }

    public string Username { get; private set; } = null!;

    public string Email { get; private set; } = null!;

    public string PasswordHash { get; private set; } = null!;

    public string? FullName { get; private set; }

    public DateTime? CreatedAt { get; private set; }

    public bool? Deleted { get; private set; }

    public string? Address { get; private set; }

    public string? Phone { get; private set; }

    public string? Ava { get; private set; }


    private readonly List<UserRoleEntity> _userRoles = new();
    public IReadOnlyCollection<UserRoleEntity> UserRoles => _userRoles;

    public UserEntity(string username, string passwordHash, string fullName, string email, DateTime createdAt, bool? deleted)
    {
        Username = username;
        PasswordHash = passwordHash;
        FullName = fullName;
        Email = email;
        CreatedAt = createdAt;
        Deleted = deleted;
    }
    public void AddRole(int roleId)
    {
        if (_userRoles.Any(r => r.RoleId == roleId))
            throw new Exception("Role duplicated!");

        _userRoles.Add(new UserRoleEntity(roleId));
    }
    public void UpdateProfile(string? username, string? fullName, string? phone, string? address, string? ava, string? passwordHash)
    {
        if (username != null)
            Username = username;

        if (fullName != null)
            FullName = fullName;

        if (phone != null)
            Phone = phone;

        if (address != null)
            Address = address;

        if (ava != null)
            Ava = ava;

        if (passwordHash != null)
            PasswordHash = passwordHash;
    }

}