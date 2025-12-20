using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class AddToCartDto
{
    [JsonPropertyName("listing_id")]
    [Required(ErrorMessage = OrderMessages.LISTING_ID_IS_REQUIRED)]
    public int? ListingId { get; set; }

    [JsonPropertyName("quantity")]
    [Required(ErrorMessage = OrderMessages.QUANTITY_IS_REQUIRED)]
    public string? Quantity { get; set; }
}