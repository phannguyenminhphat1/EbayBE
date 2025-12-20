using System.Text.Json.Serialization;

public class BuyProductDto
{
    [JsonPropertyName("ids")]
    public List<int>? Ids { get; set; }
}