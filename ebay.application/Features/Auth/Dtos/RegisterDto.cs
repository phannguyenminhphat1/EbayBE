using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class RegisterDto
{
    [JsonPropertyName("username")]
    [Required(ErrorMessage = UserMessages.USERNAME_IS_REQUIRED)]
    public string Username { get; set; } = null!;

    [JsonPropertyName("email")]
    [Required(ErrorMessage = UserMessages.EMAIL_IS_REQUIRED)]
    [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = UserMessages.EMAIL_IS_INVALID)]
    public string Email { get; set; } = null!;

    [JsonPropertyName("password")]
    [Required(ErrorMessage = UserMessages.PASSWORD_IS_REQUIRED)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*(),.?""{}|<>]).+$", ErrorMessage = UserMessages.PASSWORD_IS_INVALID)]
    public string Password { get; set; } = null!;

    [JsonPropertyName("full_name")]
    [Required(ErrorMessage = UserMessages.FULLNAME_IS_REQUIRED)]
    public string FullName { get; set; } = null!;
}