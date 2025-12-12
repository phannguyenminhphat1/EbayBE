using ebay.domain.Entities;

public interface ICategoryRepository
{
    Task<List<CategoryEntity>> GetAllCategories();
    Task<CategoryEntity?> GetCategoryById(int id);

}