using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class DeleteOrderDetailDto
{
    [JsonPropertyName("ids")]
    [Required(ErrorMessage = "Ids are required")]
    public List<int>? Ids { get; set; }
}