namespace ebay.domain.Entities;

public class OrderDetailEntity
{
    public int Id { get; private set; }
    public int ProductId { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public bool? Deleted { get; private set; }

    public OrderDetailEntity(int productId, int quantity, decimal unitPrice)
    {
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
        CreatedAt = DateTime.Now;
        Deleted = false;
    }

    public decimal GetTotal() => Quantity * UnitPrice;

    public void SoftDelete()
    {
        Deleted = true;
    }
}
