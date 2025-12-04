using System;
using System.Collections.Generic;

namespace ebay.infrastructure.Models;

public partial class PhongBan
{
    public int MaPb { get; set; }

    public string? TenPhongBan { get; set; }

    public string? DiaDiem { get; set; }

    public virtual ICollection<NhanVien> NhanViens { get; set; } = new List<NhanVien>();
}
