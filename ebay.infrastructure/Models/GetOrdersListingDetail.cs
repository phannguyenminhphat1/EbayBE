using System;
using System.Collections.Generic;

namespace ebay.infrastructure.Models;

public partial class GetOrdersListingDetail
{
    public int OrderId { get; set; }

    public int BuyerId { get; set; }

    public decimal TotalAmount { get; set; }

    public string? OrderStatus { get; set; }

    public DateTime? OrderCreatedAt { get; set; }

    public int UserId { get; set; }

    public string? FullName { get; set; }

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public string Avatar { get; set; } = null!;

    public string? OrderDetails { get; set; }
}
