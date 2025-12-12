
using AutoMapper;
using ebay.domain.Entities;
using ebay.infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class ProductListCategoryRepository(IMapper _mapper, EBayDbContext _context) : IProductListCategoryRepository
{
    public async Task<(IEnumerable<ProductListCategoryEntity> ProductListCategory, int TotalRecords)> GetProductListCategory(int? page = 1, int? pageSize = 10)
    {
        var totalRecord = await _context.ProductListCategories.CountAsync();
        var lstProductCategory = await _context.ProductListCategories
            .Skip((page!.Value - 1) * pageSize!.Value)
            .Take(pageSize.Value)
            .ToListAsync();
        var lstProductCategoryMapper = _mapper.Map<List<ProductListCategoryEntity>>(lstProductCategory);
        return (lstProductCategoryMapper, totalRecord);

    }

}