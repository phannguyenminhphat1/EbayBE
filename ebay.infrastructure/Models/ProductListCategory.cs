using System;
using System.Collections.Generic;

namespace ebay.infrastructure.Models;

public partial class ProductListCategory
{
    public int ProductId { get; set; }

    public string? ProductName { get; set; }

    public int CategoryId { get; set; }

    public string Category { get; set; } = null!;

    public decimal? Price { get; set; }

    public string? ProductImage { get; set; }
}
