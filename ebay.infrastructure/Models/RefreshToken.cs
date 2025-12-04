using System;
using System.Collections.Generic;

namespace ebay.infrastructure.Models;

public partial class RefreshToken
{
    public long Id { get; set; }

    public string Token { get; set; } = null!;

    public int UserId { get; set; }

    public string? ClientId { get; set; }

    public string? DeviceId { get; set; }

    public string? DeviceName { get; set; }

    public string? DeviceType { get; set; }

    public string? DeviceOs { get; set; }

    public string? IpAddress { get; set; }

    public DateTime? ExpiresAt { get; set; }

    public DateTime? CreatedAt { get; set; }

    public byte? Deleted { get; set; }

    public virtual Client? Client { get; set; }

    public virtual User User { get; set; } = null!;
}
