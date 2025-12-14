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

    public OrderEntity(int buyerId, string status)
    {
        BuyerId = buyerId;
        CreatedAt = DateTime.Now;
        Status = status;
        Deleted = false;
        TotalAmount = 0;
    }

    public void AddOrUpdateItem(int productId, int quantity, decimal unitPrice)
    {
        var detail = _orderDetails.SingleOrDefault(x => x.ProductId == productId && x.Deleted == false);

        if (detail == null)
        {
            _orderDetails.Add(new OrderDetailEntity(productId, quantity, unitPrice));
        }
        else
        {
            detail.IncreaseQuantity(quantity);
        }

        RecalculateTotal();
    }

    public void ChangeStatus(string status)
    {
        Status = status;
    }

    public void SoftDelete()
    {
        Deleted = true;
    }

    public void RecalculateTotal()
    {
        TotalAmount = _orderDetails.Where(x => x.Deleted == false).Sum(x => x.GetTotal());
    }
}
