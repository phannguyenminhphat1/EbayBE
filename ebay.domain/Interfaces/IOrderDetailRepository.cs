using ebay.domain.Entities;

public interface IOrderDetailRepository
{
    Task<OrderDetailEntity?> GetByOrderAndProduct(int orderId, int productId);
    Task Add(OrderDetailEntity detail);



}