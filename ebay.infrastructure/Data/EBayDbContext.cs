using System;
using System.Collections.Generic;
using ebay.infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace ebay.infrastructure.Data;

public partial class EBayDbContext : DbContext
{
    public EBayDbContext()
    {
    }

    public EBayDbContext(DbContextOptions<EBayDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Bid> Bids { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<ConnectionCountLog> ConnectionCountLogs { get; set; }

    public virtual DbSet<EbayProduct> EbayProducts { get; set; }

    public virtual DbSet<GetListOrderDetailByOrderId> GetListOrderDetailByOrderIds { get; set; }

    public virtual DbSet<GetListingProductDetail> GetListingProductDetails { get; set; }

    public virtual DbSet<GetOrdersBySeller> GetOrdersBySellers { get; set; }

    public virtual DbSet<GetOrdersListingDetail> GetOrdersListingDetails { get; set; }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<Listing> Listings { get; set; }

    public virtual DbSet<ListingProductDetail> ListingProductDetails { get; set; }

    public virtual DbSet<LoginLog> LoginLogs { get; set; }

    public virtual DbSet<NhanVien> NhanViens { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Orderby2024> Orderby2024s { get; set; }

    public virtual DbSet<Orderby2025> Orderby2025s { get; set; }

    public virtual DbSet<OrdersBy2025> OrdersBy2025s { get; set; }

    public virtual DbSet<PhongBan> PhongBans { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductImage> ProductImages { get; set; }

    public virtual DbSet<ProductListCategory> ProductListCategories { get; set; }

    public virtual DbSet<Rating> Ratings { get; set; }

    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RoleGroup> RoleGroups { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserGroup> UserGroups { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionString");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bid>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Bids__3214EC07E8D45124");

            entity.Property(e => e.BidAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Deleted).HasDefaultValue(false);

            entity.HasOne(d => d.Bidder).WithMany(p => p.Bids)
                .HasForeignKey(d => d.BidderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Bids__BidderId__07C12930");

            entity.HasOne(d => d.Listing).WithMany(p => p.Bids)
                .HasForeignKey(d => d.ListingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Bids__ListingId__06CD04F7");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Categori__3214EC07A17FBD1F");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Deleted).HasDefaultValue(false);
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Image).IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.ClientId).HasName("PK__Clients__BF21A4245908514E");

            entity.Property(e => e.ClientId)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("client_id");
            entity.Property(e => e.ClientName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("client_name");
            entity.Property(e => e.ClientType)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("client_type");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<ConnectionCountLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Connecti__3214EC07B4A9F9E4");

            entity.ToTable("ConnectionCountLog");

            entity.Property(e => e.ConnectionTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IpAddress).HasMaxLength(45);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<EbayProduct>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("EbayProduct");

            entity.Property(e => e.Category).HasMaxLength(100);
            entity.Property(e => e.Image).HasMaxLength(255);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ProductName).HasMaxLength(100);
        });

        modelBuilder.Entity<GetListOrderDetailByOrderId>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("GetListOrderDetailByOrderId");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<GetListingProductDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("GetListingProductDetail");

            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.Avatar)
                .HasMaxLength(128)
                .HasColumnName("avatar");
            entity.Property(e => e.CategoryName).HasMaxLength(100);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.CurrentPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(255);
            entity.Property(e => e.StartingPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Status).HasMaxLength(50);
        });

        modelBuilder.Entity<GetOrdersBySeller>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("GetOrdersBySeller");

            entity.Property(e => e.BuyerAddress).HasMaxLength(255);
            entity.Property(e => e.BuyerAva).IsUnicode(false);
            entity.Property(e => e.BuyerAvatar).HasMaxLength(128);
            entity.Property(e => e.BuyerEmail).HasMaxLength(100);
            entity.Property(e => e.BuyerFullName).HasMaxLength(100);
            entity.Property(e => e.BuyerPhone).HasMaxLength(255);
            entity.Property(e => e.OrderCreatedAt).HasColumnType("datetime");
            entity.Property(e => e.OrderStatus).HasMaxLength(50);
            entity.Property(e => e.SellerAddress).HasMaxLength(255);
            entity.Property(e => e.SellerAva).IsUnicode(false);
            entity.Property(e => e.SellerAvatar).HasMaxLength(128);
            entity.Property(e => e.SellerEmail).HasMaxLength(100);
            entity.Property(e => e.SellerFullName).HasMaxLength(100);
            entity.Property(e => e.SellerPhone).HasMaxLength(255);
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<GetOrdersListingDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("GetOrdersListingDetail");

            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.Avatar).HasMaxLength(128);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.OrderCreatedAt).HasColumnType("datetime");
            entity.Property(e => e.OrderStatus).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(255);
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Groups__3214EC0764FC7FB5");

            entity.HasIndex(e => e.GroupName, "UQ__Groups__6EFCD434B5176344").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Deleted).HasDefaultValue(false);
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.GroupName).HasMaxLength(50);
        });

        modelBuilder.Entity<Listing>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Listings__3214EC07D0BA2E11");

            entity.HasIndex(e => e.SellerId, "IX_Listings_SellerId");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CurrentPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Deleted).HasDefaultValue(false);
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.IsAuction).HasDefaultValue(false);
            entity.Property(e => e.StartingPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Active");
            entity.Property(e => e.Title).HasMaxLength(100);

            entity.HasOne(d => d.Category).WithMany(p => p.Listings)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Listings__Catego__0697FACD");

            entity.HasOne(d => d.Product).WithMany(p => p.Listings)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_Listings_Products");

            entity.HasOne(d => d.Seller).WithMany(p => p.Listings)
                .HasForeignKey(d => d.SellerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Listings__Seller__6477ECF3");
        });

        modelBuilder.Entity<ListingProductDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ListingProductDetail");

            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.Avatar).HasMaxLength(128);
            entity.Property(e => e.CategoryName).HasMaxLength(100);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.CurrentPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(255);
            entity.Property(e => e.StartingPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Status).HasMaxLength(50);
        });

        modelBuilder.Entity<LoginLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LoginLog__3214EC07413249BC");

            entity.ToTable("LoginLog");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Deleted).HasDefaultValue(false);
            entity.Property(e => e.IpAddress).HasMaxLength(45);
            entity.Property(e => e.IsSuccessful).HasDefaultValue(true);
            entity.Property(e => e.LoginTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.Username).HasMaxLength(100);

            entity.HasOne(d => d.User).WithMany(p => p.LoginLogs)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserId");
        });

        modelBuilder.Entity<NhanVien>(entity =>
        {
            entity.HasKey(e => e.MaNhanVien).HasName("PK__NhanVien__BDDEF20DDE204185");

            entity.ToTable("NhanVien");

            entity.Property(e => e.MaNhanVien)
                .ValueGeneratedNever()
                .HasColumnName("maNhanVien");
            entity.Property(e => e.GioiTinh)
                .HasMaxLength(10)
                .HasColumnName("gioiTinh");
            entity.Property(e => e.Luong)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("luong");
            entity.Property(e => e.PhongBan).HasColumnName("phongBan");
            entity.Property(e => e.TenNhanVien)
                .HasMaxLength(100)
                .HasColumnName("tenNhanVien");

            entity.HasOne(d => d.PhongBanNavigation).WithMany(p => p.NhanViens)
                .HasForeignKey(d => d.PhongBan)
                .HasConstraintName("FK__NhanVien__phongB__671F4F74");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Orders__3214EC07ED129322");

            entity.HasIndex(e => e.BuyerId, "IX_Orders_BuyerId");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Deleted).HasDefaultValue(false);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Pending");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Buyer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.BuyerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__BuyerId__5070F446");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OrderDet__3214EC071986CB85");

            entity.HasIndex(e => e.OrderId, "IX_OrderDetails_OrderId");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Deleted).HasDefaultValue(false);
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Listing).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ListingId)
                .HasConstraintName("FK_OrderDetails_Listings");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderDeta__Order__5535A963");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderDeta__Produ__5629CD9C");
        });

        modelBuilder.Entity<Orderby2024>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Orderby2024");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<Orderby2025>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Orderby2025");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<OrdersBy2025>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("OrdersBy2025");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<PhongBan>(entity =>
        {
            entity.HasKey(e => e.MaPb).HasName("PK__PhongBan__7A3ED78E29C939B0");

            entity.ToTable("PhongBan");

            entity.Property(e => e.MaPb)
                .ValueGeneratedNever()
                .HasColumnName("maPB");
            entity.Property(e => e.DiaDiem)
                .HasMaxLength(100)
                .HasColumnName("diaDiem");
            entity.Property(e => e.TenPhongBan)
                .HasMaxLength(100)
                .HasColumnName("tenPhongBan");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Products__3214EC0757FB8E89");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Deleted).HasDefaultValue(false);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Stock).HasDefaultValue(0);
        });

        modelBuilder.Entity<ProductImage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ProductI__3214EC07E69876DD");

            entity.HasIndex(e => e.ProductId, "IX_ProductImages_ProductId");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Deleted).HasDefaultValue(false);
            entity.Property(e => e.ImageUrl).HasMaxLength(255);
            entity.Property(e => e.IsPrimary).HasDefaultValue(false);

            entity.HasOne(d => d.Product).WithMany(p => p.ProductImages)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__ProductIm__Produ__0880433F");
        });

        modelBuilder.Entity<ProductListCategory>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ProductListCategory");

            entity.Property(e => e.Category).HasMaxLength(100);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ProductImage).HasMaxLength(255);
            entity.Property(e => e.ProductName).HasMaxLength(100);
        });

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Ratings__3214EC0751CB383E");

            entity.HasIndex(e => e.ProductId, "IX_Ratings_ProductId");

            entity.HasIndex(e => e.RatedUserId, "IX_Ratings_RatedUserId");

            entity.HasIndex(e => e.RaterId, "IX_Ratings_RaterId");

            entity.Property(e => e.Comment).HasMaxLength(500);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Deleted).HasDefaultValue(false);

            entity.HasOne(d => d.Product).WithMany(p => p.Ratings)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__Ratings__Product__5DCAEF64");

            entity.HasOne(d => d.RatedUser).WithMany(p => p.RatingRatedUsers)
                .HasForeignKey(d => d.RatedUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ratings__RatedUs__5CD6CB2B");

            entity.HasOne(d => d.Rater).WithMany(p => p.RatingRaters)
                .HasForeignKey(d => d.RaterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ratings__RaterId__5BE2A6F2");
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Refresh___3213E83FEC267AF6");

            entity.ToTable("Refresh_tokens");

            entity.HasIndex(e => e.Token, "UQ__Refresh___CA90DA7A4B1BDF21").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClientId)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("client_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Deleted)
                .HasDefaultValue((byte)0)
                .HasColumnName("deleted");
            entity.Property(e => e.DeviceId)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("device_id");
            entity.Property(e => e.DeviceName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("device_name");
            entity.Property(e => e.DeviceOs)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("device_os");
            entity.Property(e => e.DeviceType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("device_type");
            entity.Property(e => e.ExpiresAt)
                .HasColumnType("datetime")
                .HasColumnName("expires_at");
            entity.Property(e => e.IpAddress)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("ip_address");
            entity.Property(e => e.Token)
                .HasMaxLength(500)
                .HasColumnName("token");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Client).WithMany(p => p.RefreshTokens)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("FK__Refresh_t__clien__55009F39");

            entity.HasOne(d => d.User).WithMany(p => p.RefreshTokens)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Refresh_t__user___540C7B00");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Roles__3214EC074F1993A2");

            entity.HasIndex(e => e.RoleName, "UQ__Roles__8A2B6160924EDB9B").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Deleted).HasDefaultValue(false);
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<RoleGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RoleGrou__3214EC07740E5A31");

            entity.HasIndex(e => e.GroupId, "IX_RoleGroups_GroupId");

            entity.HasIndex(e => new { e.RoleId, e.GroupId }, "UC_RoleGroup").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Deleted).HasDefaultValue(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07498664A6");

            entity.HasIndex(e => e.FullName, "IX_Users_FullName");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E491807BE2").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D105347DE540EE").IsUnique();

            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.Ava).IsUnicode(false);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Deleted).HasDefaultValue(false);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(255);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        modelBuilder.Entity<UserGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserGrou__3214EC077C3F4B72");

            entity.HasIndex(e => e.UserId, "IX_UserGroups_UserId");

            entity.HasIndex(e => new { e.UserId, e.GroupId }, "UC_UserGroup").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Deleted).HasDefaultValue(false);
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.RoleId });

            entity.ToTable("UserRole");

            entity.Property(e => e.Description).HasMaxLength(255);

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRole_Roles");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRole_Users");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
