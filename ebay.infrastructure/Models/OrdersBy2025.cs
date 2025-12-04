using System;
using System.Collections.Generic;

namespace ebay.infrastructure.Models;

public partial class OrdersBy2025
{
    public int Id { get; set; }

    public int BuyerId { get; set; }

    public decimal TotalAmount { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? Deleted { get; set; }
}
