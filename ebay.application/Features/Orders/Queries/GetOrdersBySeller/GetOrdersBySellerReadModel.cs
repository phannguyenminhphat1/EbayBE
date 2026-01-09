public class GetOrdersBySellerReadModel
{
    public int OrderId { get; set; }

    public int BuyerId { get; set; }

    public decimal TotalAmount { get; set; }

    public string OrderStatus { get; set; } = null!;

    public DateTime? OrderCreatedAt { get; set; }


    // ===== BUYER INFO =====
    public int BuyerUserId { get; set; }

    public string? BuyerFullName { get; set; }

    public string BuyerEmail { get; set; } = null!;

    public string? BuyerPhone { get; set; }

    public string? BuyerAddress { get; set; }

    public string BuyerAvatar { get; set; } = null!;

    public string? BuyerAva { get; set; }


    // ===== SELLER INFO =====
    public int SellerId { get; set; }

    public string? SellerFullName { get; set; }

    public string SellerEmail { get; set; } = null!;

    public string? SellerPhone { get; set; }

    public string? SellerAddress { get; set; }

    public string SellerAvatar { get; set; } = null!;

    public string? SellerAva { get; set; }

    public string? OrderDetails { get; set; }

}


public class GetOrderDetailsBySellerReadModel
{
    public int OrderDetailId { get; set; }

    public int ListingId { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public DateTime? CreatedAt { get; set; }

    // ====== LISTING ========
    public string ListingTitle { get; set; } = null!;

    public string ListingStatus { get; set; } = null!;

    public DateTime? ListingCreatedAt { get; set; }

    // ===== PRODUCT (derived from listing) =====
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public string? Description { get; set; }

    public int Stock { get; set; }
    public List<GetProductImageBySellerReadModel> ProductImages { get; set; } = [];


}

public class GetProductImageBySellerReadModel
{
    public int Id { get; set; }

    public string? ImageUrl { get; set; }
}