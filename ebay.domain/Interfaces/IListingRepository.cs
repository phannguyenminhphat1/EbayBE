using ebay.domain.Entities;

public interface IListingRepository
{
    Task<ListingEntity?> GetById(int id);
    Task Add(ListingEntity entity);

    Task<List<ListingEntity>?> GetByIds(List<int> ids, int userId, string userRole);
    Task Update(ListingEntity listingEntity);

    Task SoftDeleteByIds(List<int> ids, int userId, string userRole);

}