using ebay.domain.Entities;

public interface IOrderDetailRepository
{
    Task<OrderDetailEntity?> GetByOrderAndProduct(int orderId, int productId);
    Task Add(OrderDetailEntity detail);

    Task<OrderDetailEntity?> GetById(int id);
    Task<List<OrderDetailEntity>> GetByIds(List<int> ids);

    Task DeleteOrderDetails(List<OrderDetailEntity> entities);

}