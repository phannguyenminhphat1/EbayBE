using System.Text.Json.Serialization;

public class OrdersDetailListingDetailReadModel
{
    public int OrderDetailId { get; set; }

    public int ListingId { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public DateTime? CreatedAt { get; set; }

    // ====== LISTING ========
    public int SellerId { get; set; }

    public string ListingTitle { get; set; } = null!;

    public string ListingStatus { get; set; } = null!;

    public DateTime? ListingCreatedAt { get; set; }

    // ===== PRODUCT (derived from listing) =====
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public string? Description { get; set; }

    public int Stock { get; set; }

    // ===== SELLER INFO =====
    public int SellerUserId { get; set; }

    public string? SellerFullName { get; set; }

    public string SellerEmail { get; set; } = null!;

    public string? SellerPhone { get; set; }

    public string? SellerAddress { get; set; }

    public string SellerAvatar { get; set; } = null!;


    public List<ProductImageReadModel> ProductImages { get; set; } = [];
}
