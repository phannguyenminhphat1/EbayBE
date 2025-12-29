using ebay.domain.Entities;

public interface IOrderRepository
{
    Task<OrderEntity?> GetOrderInCartById(int buyerId);
    Task<OrderEntity?> GetOrderById(int buyerId, string? status, int id);
    Task Add(OrderEntity orderEntity);
    Task Update(OrderEntity orderEntity);
    Task<OrderEntity?> GetByOrderDetailIds(List<int> detailIds, int buyerId);




}