using ebay.domain.Entities;

public class UserRoleEntity
{
    public int RoleId { get; private set; }
    public string? Description { get; private set; }

    public string? RoleName { get; private set; }

    public UserRoleEntity(int roleId)
    {
        RoleId = roleId;
    }
}