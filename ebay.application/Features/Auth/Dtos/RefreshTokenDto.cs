using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class RefreshTokenDto
{
    [JsonPropertyName("access_token")]
    [Required(ErrorMessage = AuthMessages.ACCESS_TOKEN_IS_REQUIRED)]
    public string? AccessToken { get; set; }

    [JsonPropertyName("refresh_token")]
    [Required(ErrorMessage = UserMessages.REFRESH_TOKEN_IS_REQUIRED)]
    public string? RefreshToken { get; set; }
}