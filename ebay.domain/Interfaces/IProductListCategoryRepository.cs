using ebay.domain.Entities;

public interface IProductListCategoryRepository
{
    Task<(IEnumerable<ProductListCategoryEntity> ProductListCategory, int TotalRecords)> GetProductListCategory(int? page, int? pageSize = 10);

}