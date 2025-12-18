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

    public void RemoveOrderDetails(List<int> orderDetailIds)
    {
        var detailsToRemove = _orderDetails.Where(x => orderDetailIds.Contains(x.Id) && x.Deleted == false).ToList();
        if (detailsToRemove.Count == 0) return;
        foreach (var detail in detailsToRemove)
        {
            detail.SoftDelete();
        }
        RecalculateTotal();
    }

    public void UpdateOrderDetailQuantity(int productId, int quantity)
    {
        var detail = _orderDetails.SingleOrDefault(x => x.ProductId == productId && x.Deleted == false);

        if (detail == null) throw new Exception("Order detail of product not found");

        if (quantity <= 0) throw new Exception("Quantity must be positive");

        detail.SetQuantity(quantity);

        RecalculateTotal();
    }


}
