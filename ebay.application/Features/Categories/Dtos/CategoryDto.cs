using System.Text.Json.Serialization;


public class CategoryDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;

    [JsonPropertyName("image")]
    public string Image { get; set; } = null!;


}