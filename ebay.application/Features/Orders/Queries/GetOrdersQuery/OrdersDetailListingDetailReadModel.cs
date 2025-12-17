using System.Text.Json.Serialization;

public class OrdersDetailListingDetailReadModel
{
    public int OrderDetailId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public string ProductName { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int Stock { get; set; }

    public List<ProductImageReadModel> ProductImages { get; set; } = [];
}