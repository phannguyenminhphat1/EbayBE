using AutoMapper;
using ebay.domain.Entities;
using ebay.infrastructure.Data;
using ebay.infrastructure.Models;
using Microsoft.EntityFrameworkCore;

public class ProductRepository(EBayDbContext _context, IMapper _mapper) : IProductRepository
{
    public async Task<(IEnumerable<ProductEntity> ListProducts, int TotalRecords)> GetAllProducts(int page, int? pageSize = 10, string? keyword = null, int? category = null)
    {
        var query = _context.Products.Include(p => p.ProductImages).AsQueryable();

        throw new NotImplementedException();
    }

    public Task<(IEnumerable<ProductEntity> ListProducts, int TotalRecords)> GetAllProducts()
    {
        throw new NotImplementedException();
    }
}