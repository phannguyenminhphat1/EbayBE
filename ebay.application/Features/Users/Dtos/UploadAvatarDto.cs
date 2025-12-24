using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

public class UploadAvatarDto
{
    [JsonPropertyName("ava")]
    [FromForm(Name = "ava")]
    [Required(ErrorMessage = UserMessages.IMAGE_IS_REQUIRED)]
    public IFormFile? Ava { get; set; }
}
