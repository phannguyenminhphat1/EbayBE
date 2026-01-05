using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class DeleteListingDto
{
    [JsonPropertyName("ids")]
    [Required(ErrorMessage = "Ids are required")]
    public List<int>? Ids { get; set; }
}