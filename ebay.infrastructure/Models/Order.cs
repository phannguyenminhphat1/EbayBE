using System;
using System.Collections.Generic;

namespace ebay.infrastructure.Models;

public partial class Order
{
    public int Id { get; set; }

    public int BuyerId { get; set; }

    public decimal TotalAmount { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? Deleted { get; set; }

    public virtual User Buyer { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
