using System;
using System.Collections.Generic;

namespace ebay.infrastructure.Models;

public partial class Client
{
    public string ClientId { get; set; } = null!;

    public string ClientName { get; set; } = null!;

    public string? ClientType { get; set; }

    public string? Description { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
}
