using System;
using System.Collections.Generic;

namespace ebay.infrastructure.Models;

public partial class Rating
{
    public int Id { get; set; }

    public int RaterId { get; set; }

    public int RatedUserId { get; set; }

    public int? ProductId { get; set; }

    public int RatingScore { get; set; }

    public string? Comment { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? Deleted { get; set; }

    public virtual Product? Product { get; set; }

    public virtual User RatedUser { get; set; } = null!;

    public virtual User Rater { get; set; } = null!;
}
