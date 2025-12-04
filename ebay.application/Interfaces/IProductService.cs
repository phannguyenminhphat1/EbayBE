namespace ebay.application.Interfaces;

public interface IProductService
{
    Task<IEnumerable<GetProductDto>> GetAllProducts();
}