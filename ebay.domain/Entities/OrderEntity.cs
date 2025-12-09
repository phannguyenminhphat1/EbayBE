namespace ebay.domain.Entities;

public class OrderEntity
{
    public int Id { get; private set; }
    public int BuyerId { get; private set; }
    public decimal TotalAmount { get; private set; }
    public string? Status { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public bool? Deleted { get; private set; }

    private readonly List<OrderDetailEntity> _orderDetails = new();
    public IReadOnlyCollection<OrderDetailEntity> OrderDetails => _orderDetails;

    public OrderEntity(int buyerId)
    {
        BuyerId = buyerId;
        CreatedAt = DateTime.Now;
        Status = "Pending";
        Deleted = false;
        TotalAmount = 0;
    }

    public void AddOrderDetail(int productId, int quantity, decimal unitPrice)
    {
        var detail = new OrderDetailEntity(productId, quantity, unitPrice);
        _orderDetails.Add(detail);

        TotalAmount += quantity * unitPrice;
    }

    public void ChangeStatus(string status)
    {
        Status = status;
    }

    public void SoftDelete()
    {
        Deleted = true;
    }
}
