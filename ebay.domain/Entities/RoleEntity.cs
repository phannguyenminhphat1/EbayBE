public class RoleEntity
{
    public int Id { get; private set; }

    public string RoleName { get; private set; } = null!;
    public bool? Deleted { get; private set; }

    public RoleEntity(int id, string roleName, bool? deleted)
    {
        Id = id;
        RoleName = roleName;
        Deleted = deleted;
    }

}