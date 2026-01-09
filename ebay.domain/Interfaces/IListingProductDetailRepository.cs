using ebay.domain.Entities;

public interface IListingProductDetailRepository
{

    Task<(IEnumerable<ListingProductDetailEntity> ListListingProductDetail, int TotalRecords)> GetListListingProductDetail(
        int? currentUserId,
        int? page = 1,
        int? pageSize = 10,
        string? name = null,
        string? categoryId = null,
        string? priceMin = null,
        string? priceMax = null,
        string? ratingFilter = null,
        string? order = null,
        string? sortBy = null
    );
    Task<(IEnumerable<ListingProductDetailEntity> ListListing, int TotalRecords)> GetListListingPost(int userId, string userRole, int? page = 1, int? pageSize = 10, string? name = null, string? order = null, string? status = null);
    Task<ListingProductDetailEntity?> GetListingProductDetailById(int id);

}