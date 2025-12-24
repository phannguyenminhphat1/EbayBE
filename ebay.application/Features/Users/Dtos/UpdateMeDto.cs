using System.Text.Json.Serialization;

public class UpdateMeDto
{
    [JsonPropertyName("username")]
    public string? Username { get; set; }

    [JsonPropertyName("fullname")]
    public string? FullName { get; set; }

    [JsonPropertyName("password_hash")]
    public string? PasswordHash { get; set; }

    [JsonPropertyName("address")]
    public string? Address { get; set; }

    [JsonPropertyName("phone")]
    public string? Phone { get; set; }

    [JsonPropertyName("ava")]
    public string? Ava { get; set; }
}