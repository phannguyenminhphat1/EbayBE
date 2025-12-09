using AutoMapper;
using ebay.domain.Entities;
using ebay.infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class CategoryRepository(EBayDbContext _context, IMapper _mapper) : ICategoryRepository
{
    public async Task<List<CategoryEntity>> GetAllCategories()
    {
        var lstCategories = await _context.Categories.ToListAsync();
        var lstCategoriesMapper = _mapper.Map<List<CategoryEntity>>(lstCategories);
        return lstCategoriesMapper;

    }
}