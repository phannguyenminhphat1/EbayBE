using ebay.domain.Entities;

public interface IProductRepository
{
    Task<(IEnumerable<ProductEntity> ListProducts, int TotalRecords)> GetAllProducts();
}