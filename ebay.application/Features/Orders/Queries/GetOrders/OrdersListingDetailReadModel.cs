using System.Text.Json.Serialization;

namespace ebay.application.ReadModels;

public class OrdersListingDetailReadModel
{
    public int OrderId { get; set; }

    public int BuyerId { get; set; }

    public decimal TotalAmount { get; set; }

    public string OrderStatus { get; set; } = null!;

    public DateTime? OrderCreatedAt { get; set; }

    // ===== BUYER INFO =====
    public int UserId { get; set; }

    public string? FullName { get; set; }

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public string Avatar { get; set; } = null!;

    // ===== RAW JSON từ VIEW =====
    public string? OrderDetails { get; set; }
}
