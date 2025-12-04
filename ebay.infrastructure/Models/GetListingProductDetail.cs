using System;
using System.Collections.Generic;

namespace ebay.infrastructure.Models;

public partial class GetListingProductDetail
{
    public DateTime? CreatedAt { get; set; }

    public bool? Deleted { get; set; }

    public int Id { get; set; }

    public int ProductId { get; set; }

    public string Avatar { get; set; } = null!;

    public string? Address { get; set; }

    public string? Description { get; set; }

    public string? Status { get; set; }

    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public string Name { get; set; } = null!;

    public decimal? CurrentPrice { get; set; }

    public decimal StartingPrice { get; set; }

    public int UserId { get; set; }

    public string? FullName { get; set; }

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public int? GroupListing { get; set; }

    public string? ListImageDetail { get; set; }
}
