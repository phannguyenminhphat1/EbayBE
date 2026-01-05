using System;
using System.Collections.Generic;

namespace ebay.infrastructure.Models;

public partial class GetOrdersBySeller
{
    public int OrderId { get; set; }

    public int BuyerId { get; set; }

    public decimal TotalAmount { get; set; }

    public string? OrderStatus { get; set; }

    public DateTime? OrderCreatedAt { get; set; }

    public int BuyerUserId { get; set; }

    public string? BuyerFullName { get; set; }

    public string BuyerEmail { get; set; } = null!;

    public string? BuyerPhone { get; set; }

    public string? BuyerAddress { get; set; }

    public string BuyerAvatar { get; set; } = null!;

    public string? BuyerAva { get; set; }

    public int SellerId { get; set; }

    public string? SellerFullName { get; set; }

    public string SellerEmail { get; set; } = null!;

    public string? SellerPhone { get; set; }

    public string? SellerAddress { get; set; }

    public string SellerAvatar { get; set; } = null!;

    public string? SellerAva { get; set; }

    public string? OrderDetails { get; set; }
}
