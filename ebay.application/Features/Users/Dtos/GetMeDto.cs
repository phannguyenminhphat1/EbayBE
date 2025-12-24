using System.Text.Json.Serialization;

public class GetMeDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("username")]
    public string? Username { get; set; }

    [JsonPropertyName("fullname")]
    public string? FullName { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("address")]
    public string? Address { get; set; }

    [JsonPropertyName("phone")]
    public string? Phone { get; set; }

    [JsonPropertyName("ava")]
    public string? Ava { get; set; }

    [JsonPropertyName("user_roles")]
    public List<UserRoleDto> UserRoles { get; set; } = [];
}