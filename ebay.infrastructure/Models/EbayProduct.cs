using System;
using System.Collections.Generic;

namespace ebay.infrastructure.Models;

public partial class EbayProduct
{
    public int Id { get; set; }

    public string Image { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public string Category { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int Stock { get; set; }
}
