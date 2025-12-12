using System;
using System.Collections.Generic;

namespace ebay.infrastructure.Models;

public partial class ListingProductDetail
{
    public int Id { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? Deleted { get; set; }

    public string? Status { get; set; }

    public int? CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public int UserId { get; set; }

    public string? FullName { get; set; }

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public string Avatar { get; set; } = null!;

    public int ProductId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public decimal? StartingPrice { get; set; }

    public decimal? CurrentPrice { get; set; }

    public string? ListImageDetail { get; set; }

    public int? GroupListing { get; set; }

    public int? TotalSold { get; set; }

    public int? AverageRatingScore { get; set; }
}
