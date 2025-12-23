using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class UpdateOrderDetailDto
{
    [JsonPropertyName("order_detail_id")]
    [Required(ErrorMessage = OrderMessages.ORDER_DETAIL_ID_IS_REQUIRED)]
    public int? Id { get; set; }

    [JsonPropertyName("quantity")]
    [Required(ErrorMessage = OrderMessages.QUANTITY_IS_REQUIRED)]
    public string? Quantity { get; set; }
}