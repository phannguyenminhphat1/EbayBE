using AutoMapper;
using ebay.domain.Entities;
using ebay.infrastructure.Data;
using ebay.infrastructure.Models;
using Microsoft.EntityFrameworkCore;

public class ProductRepository : IProductRepository
{
    private readonly EBayDbContext _context;
    private readonly IMapper _mapper;

    public ProductRepository(EBayDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<ProductEntity?> GetById(int id)
    {
        var product = await _context.Products.SingleOrDefaultAsync(p => p.Id == id && p.Deleted == false);
        return product == null ? null : _mapper.Map<ProductEntity>(product);
    }
}