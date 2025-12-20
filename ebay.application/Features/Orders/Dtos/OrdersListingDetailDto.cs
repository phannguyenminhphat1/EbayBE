using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

public class OrdersListingDetailDto
{
    [JsonPropertyName("order_id")]
    public int OrderId { get; set; }

    [JsonPropertyName("total_amount")]
    public decimal TotalAmount { get; set; }

    [JsonPropertyName("order_status")]
    public string? OrderStatus { get; set; }

    [JsonPropertyName("order_created_at")]
    public DateTime? OrderCreatedAt { get; set; }

    [JsonPropertyName("buyer")]
    public BuyerDto? Buyer { get; set; }

    [JsonPropertyName("order_details")]
    public List<OrderDetailsDto>? OrderDetails { get; set; }
}

public class OrderDetailsDto
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

    [JsonPropertyName("seller")]
    public SellerDto? Seller { get; set; }

    [JsonPropertyName("product")]
    public ProductDto? Product { get; set; }
}

public class ProductImageDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("image_url")]
    public string? ImageUrl { get; set; }
}

public class BuyerDto
{
    [JsonPropertyName("user_id")]
    public int UserId { get; set; }

    [JsonPropertyName("fullname")]
    public string? FullName { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; } = null!;

    [JsonPropertyName("phone")]
    public string? Phone { get; set; }

    [JsonPropertyName("address")]
    public string? Address { get; set; }

    [JsonPropertyName("avatar")]
    public string Avatar { get; set; } = null!;
}

public class SellerDto
{
    [JsonPropertyName("seller_user_id")]
    public int SellerUserId { get; set; }

    [JsonPropertyName("seller_fullname")]
    public string? SellerFullName { get; set; }

    [JsonPropertyName("seller_email")]
    public string SellerEmail { get; set; } = null!;

    [JsonPropertyName("seller_phone")]
    public string? SellerPhone { get; set; }

    [JsonPropertyName("seller_address")]
    public string? SellerAddress { get; set; }

    [JsonPropertyName("seller_avatar")]
    public string SellerAvatar { get; set; } = null!;
}

public class ListingDto
{
    [JsonPropertyName("listing_title")]
    public string ListingTitle { get; set; } = null!;

    [JsonPropertyName("listing_status")]
    public string ListingStatus { get; set; } = null!;

    [JsonPropertyName("listing_created_at")]
    public DateTime? ListingCreatedAt { get; set; }
}

public class ProductDto
{
    [JsonPropertyName("product_id")]
    public int ProductId { get; set; }

    [JsonPropertyName("product_name")]
    public string ProductName { get; set; } = null!;

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("stock")]
    public int Stock { get; set; }

    [JsonPropertyName("product_image")]
    public List<ProductImageDto> ProductImages { get; set; } = [];
}