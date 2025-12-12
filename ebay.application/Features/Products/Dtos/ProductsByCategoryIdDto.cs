using System.Text.Json.Serialization;

public class ProductsByCategoryIdDto
{
    [JsonPropertyName("product_id")]
    public int ProductId { get; set; }

    [JsonPropertyName("product_name")]
    public string? ProductName { get; set; }

    [JsonPropertyName("price")]
    public decimal? Price { get; set; }

    [JsonPropertyName("product_image")]
    public string? ProductImage { get; set; }

    [JsonPropertyName("total_sold")]
    public int TotalSold { get; set; }

    [JsonPropertyName("average_rating_score")]
    public int AverageRatingScore { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime? CreatedAt { get; set; }

}