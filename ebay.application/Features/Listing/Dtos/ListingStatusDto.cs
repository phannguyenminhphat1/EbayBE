using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class ListingStatusDto
{
    [JsonPropertyName("id")]
    [Required(ErrorMessage = "Listing id is required")]
    public int? Id { get; set; }


    [JsonPropertyName("status")]
    [Required(ErrorMessage = "Status is required")]
    public string? Status { get; set; }
}