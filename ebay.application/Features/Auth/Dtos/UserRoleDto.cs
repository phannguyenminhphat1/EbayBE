using System.Text.Json.Serialization;

public class UserRoleDto
{
    [JsonPropertyName("role_id")]
    public int RoleId { get; set; }

    [JsonPropertyName("role_name")]
    public string? RoleName { get; set; }


}