using ebay.domain.Entities;

public class UserRoleEntity
{
    public int UserId { get; set; }
    public int RoleId { get; set; }
    public string? Description { get; set; }
    // public  Role Role { get; set; } = null!;
    public UserEntity User { get; } = null!;
    private UserRoleEntity() { }

    public UserRoleEntity(int userId, int roleId)
    {
        UserId = userId;
        RoleId = roleId;
    }
}