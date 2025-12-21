using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class BuyProductsDto
{
    [JsonPropertyName("ids")]
    public List<int>? Ids { get; set; }
}