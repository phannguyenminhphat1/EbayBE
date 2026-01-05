using ebay.domain.Entities;

public interface IProductRepository
{

    Task<ProductEntity?> GetById(int id);
    Task Add(ProductEntity entity);

    Task SoftDeleteByListingIds(List<int> listingProductIds);

}