using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class RefreshTokenDto
{
    [JsonPropertyName("refresh_token")]
    [Required(ErrorMessage = UserMessages.REFRESH_TOKEN_IS_REQUIRED)]
    public string? RefreshToken { get; set; }
}