using System.Text.Json.Serialization;

public class GetOrderBySellerDto
{
    [JsonPropertyName("order_id")]
    public int OrderId { get; set; }

    [JsonPropertyName("buyer_id")]
    public int BuyerId { get; set; }

    [JsonPropertyName("total_amount")]
    public decimal TotalAmount { get; set; }

    [JsonPropertyName("order_status")]
    public string? OrderStatus { get; set; }

    [JsonPropertyName("order_created_at")]
    public DateTime? OrderCreatedAt { get; set; }

    [JsonPropertyName("buyer")]
    public GetOrderBuyerDto? Buyer { get; set; }

    [JsonPropertyName("seller")]
    public GetOrderSellerDto? Seller { get; set; }

    [JsonPropertyName("order_details")]
    public List<GetSellerOrderDetailsDto>? OrderDetails { get; set; }

}

public class GetOrderBuyerDto
{
    [JsonPropertyName("buyer_fullname")]
    public string? BuyerFullName { get; set; }

    [JsonPropertyName("buyer_email")]
    public string BuyerEmail { get; set; } = null!;

    [JsonPropertyName("buyer_phone")]
    public string? BuyerPhone { get; set; }

    [JsonPropertyName("buyer_address")]
    public string? BuyerAddress { get; set; }

    [JsonPropertyName("buyer_avatar")]
    public string BuyerAvatar { get; set; } = null!;

    [JsonPropertyName("buyer_ava")]
    public string? BuyerAva { get; set; }
}


public class GetOrderSellerDto
{
    [JsonPropertyName("seller_id")]
    public int SellerId { get; set; }

    [JsonPropertyName("seller_fullname")]
    public string? SellerFullName { get; set; }

    [JsonPropertyName("seller_email")]
    public string SellerEmail { get; set; } = null!;

    [JsonPropertyName("seller_phone")]
    public string? SellerPhone { get; set; }

    [JsonPropertyName("seller_address")]
    public string? SellerAddress { get; set; }

    [JsonPropertyName("seller_avatar")]
    public string? SellerAvatar { get; set; }

    [JsonPropertyName("seller_ava")]
    public string? SellerAva { get; set; }
}

public class GetSellerOrderDetailsDto
{
    [JsonPropertyName("order_detail_id")]
    public int OrderDetailId { get; set; }

    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }

    [JsonPropertyName("unit_price")]
    public decimal UnitPrice { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime? CreatedAt { get; set; }

    [JsonPropertyName("listing")]
    public ListingDto? Listing { get; set; }

    [JsonPropertyName("product")]
    public ProductDto? Product { get; set; }
}