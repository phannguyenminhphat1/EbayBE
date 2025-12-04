using ebay.domain.Entities;
using ebay.infrastructure.Data;
using ebay.infrastructure.Models;
using Microsoft.EntityFrameworkCore;

public class ProductRepository : IProductRepository
{
    protected readonly EBayDbContext _context;
    protected readonly DbSet<Product> _dbSet;

    public ProductRepository(EBayDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<Product>();
    }
    public async Task<IEnumerable<ProductEntity>> GetAllProducts()
    {
        return await _dbSet.Select(p => new ProductEntity(
                p.Name!,
                p.Price!.Value,
                p.Stock!.Value
            ))
            .ToListAsync();
    }
}