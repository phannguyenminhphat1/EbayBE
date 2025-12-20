using ebay.domain.Entities;

public interface IListingRepository
{
    Task<ListingEntity?> GetById(int id);
}