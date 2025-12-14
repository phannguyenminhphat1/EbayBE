using ebay.domain.Entities;

public interface IProductRepository
{

    Task<ProductEntity?> GetById(int id);
}