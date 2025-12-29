// Application layer
public interface IOrderDetailQuery
{
    Task<List<OrderDetailWithSellerDto>> GetByIdsWithSeller(List<int> ids);
}
