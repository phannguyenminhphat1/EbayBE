using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class LoginDto
{
    [JsonPropertyName("email")]
    [Required(ErrorMessage = UserMessages.EMAIL_IS_REQUIRED)]
    public string Email { get; set; } = null!;

    [JsonPropertyName("password")]
    [Required(ErrorMessage = UserMessages.PASSWORD_IS_REQUIRED)]
    public string Password { get; set; } = null!;
}