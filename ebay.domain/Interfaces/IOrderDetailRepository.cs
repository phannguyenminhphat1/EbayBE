using ebay.domain.Entities;

public interface IOrderDetailRepository
{
    Task<OrderDetailEntity?> GetByOrderIdAndListingId(int orderId, int listingId);
    Task<OrderDetailEntity?> GetById(int id);
    Task<List<OrderDetailEntity>> GetByIds(List<int> ids);


}