using System;
using System.Collections.Generic;

namespace ebay.domain.Entities;

public class ListingProductDetailEntity
{
    public int Id { get; private set; }

    public DateTime? CreatedAt { get; private set; }

    public bool? Deleted { get; private set; }

    public string? Status { get; private set; }

    public int? CategoryId { get; private set; }

    public string CategoryName { get; private set; } = null!;

    public int UserId { get; private set; }

    public string? FullName { get; private set; }

    public string Email { get; private set; } = null!;

    public string? Phone { get; private set; }

    public string? Address { get; private set; }

    public string Avatar { get; private set; } = null!;

    public int ProductId { get; private set; }

    public string? Name { get; private set; }

    public string? Description { get; private set; }

    public decimal? StartingPrice { get; private set; }

    public decimal? CurrentPrice { get; private set; }

    public string? ListImageDetail { get; private set; }

    public int? Stock { get; private set; }

    public int? GroupListing { get; private set; }

    public int? TotalSold { get; private set; }

    public int? AverageRatingScore { get; private set; }
}
