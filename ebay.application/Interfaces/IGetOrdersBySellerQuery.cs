using ebay.application.ReadModels;
namespace ebay.application.Interfaces;

public interface IGetOrdersBySellerQuery
{
    Task<(IEnumerable<GetOrdersBySellerReadModel> Orders, int TotalRecords)> GetOrdersBySeller(int sellerId, string? status, int? page, int? pageSize);

}

