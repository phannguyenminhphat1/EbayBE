using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class RejectAndConfirmDto
{
    [JsonPropertyName("is_confirmed")]
    public bool IsConfirmed { get; set; }
}