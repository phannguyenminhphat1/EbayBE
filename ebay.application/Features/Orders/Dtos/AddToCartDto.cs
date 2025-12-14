using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class AddToCartDto
{
    [JsonPropertyName("product_id")]
    [Required(ErrorMessage = ProductMessages.PRODUCT_ID_IS_REQUIRED)]
    public int? ProductId { get; set; }

    [JsonPropertyName("quantity")]
    [Required(ErrorMessage = OrderMessages.QUANTITY_IS_REQUIRED)]
    public string? Quantity { get; set; }
}