using System.Text.Json.Serialization;

public class UpdateListingDto
{
    [JsonPropertyName("category_id")]
    public int? CategoryId { get; set; }

    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("listing_description")]
    public string? ListingDescription { get; set; }

    [JsonPropertyName("product_name")]
    public string ProductName { get; set; } = null!;

    [JsonPropertyName("description")]
    public string? ProductDescription { get; set; }

    [JsonPropertyName("starting_price")]
    public decimal? StartingPrice { get; set; }

    [JsonPropertyName("stock")]
    public int Stock { get; set; }

    [JsonPropertyName("images")]
    public List<string>? Images { get; set; }
}