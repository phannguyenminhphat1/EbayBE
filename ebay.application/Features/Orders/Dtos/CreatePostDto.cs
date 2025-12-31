using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class CreatePostDto
{
    [JsonPropertyName("category_id")]
    [Required(ErrorMessage = ListingMessages.CATEGORY_ID_IS_REQUIRED)]
    public int? CategoryId { get; set; }

    [JsonPropertyName("title")]
    [Required(ErrorMessage = ListingMessages.TITLE_IS_REQUIRED)]
    public string? Title { get; set; }

    [JsonPropertyName("listing_description")]
    [Required(ErrorMessage = ListingMessages.LISTING_DESCRIPTION_IS_REQUIRED)]
    public string? ListingDescription { get; set; }

    [JsonPropertyName("product_name")]
    [Required(ErrorMessage = ListingMessages.PRODUCT_NAME_IS_REQUIRED)]
    public string ProductName { get; set; } = null!;

    [JsonPropertyName("description")]
    [Required(ErrorMessage = ListingMessages.PRODUCT_DESCRIPTION_IS_REQUIRED)]
    public string? ProductDescription { get; set; }

    [JsonPropertyName("starting_price")]
    [Required(ErrorMessage = ListingMessages.PRICE_IS_REQUIRED)]
    public decimal? StartingPrice { get; set; }

    [JsonPropertyName("stock")]
    [Required(ErrorMessage = ListingMessages.STOCK_IS_REQUIRED)]
    public int Stock { get; set; }

    [JsonPropertyName("images")]
    [Required(ErrorMessage = ListingMessages.IMAGES_IS_REQUIRED)]
    public List<string>? Images { get; set; }

    [JsonPropertyName("is_auction")]
    public bool IsAuction { get; set; } = false;

    [JsonPropertyName("end_date")]
    public DateTime? EndDate { get; set; }

}