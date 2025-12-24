using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class ChangePasswordDto
{
    [JsonPropertyName("old_password")]
    [Required(ErrorMessage = UserMessages.OLD_PASSWORD_IS_REQUIRED)]
    public string? OldPassword { get; set; }

    [JsonPropertyName("new_password")]
    [Required(ErrorMessage = UserMessages.NEW_PASSWORD_IS_REQUIRED)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*(),.?""{}|<>]).+$", ErrorMessage = UserMessages.PASSWORD_IS_INVALID)]
    public string? NewPassword { get; set; }

    [JsonPropertyName("confirm_password")]
    [Required(ErrorMessage = UserMessages.CONFIRM_PASSWORD_IS_REQUIRED)]
    public string? ConfirmPassword { get; set; }
}