using ebay.application.ReadModels;
namespace ebay.application.Interfaces;

public interface IOrdersListingDetailRepository
{
    Task<(IEnumerable<OrdersListingDetailReadModel> Orders, int TotalRecords)> GetOrdersListingDetail(int buyerId, string? status, int? page, int? pageSize);
}

