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

    public async Task Add(ProductEntity entity)
    {
        var product = new Product
        {
            Deleted = false,
            Price = entity.Price,
            CreatedAt = entity.CreatedAt,
            Description = entity.Description,
            Stock = entity.Stock,
            Name = entity.Name
        };
        foreach (var item in entity.Images)
        {
            product.ProductImages.Add(new ProductImage
            {
                Deleted = false,
                ImageUrl = item.ImageUrl,
                CreatedAt = item.CreatedAt,
                IsPrimary = false
            });
        }
        await _context.Products.AddAsync(product);
    }


    public async Task SoftDeleteByListingIds(List<int> listingProductIds)
    {
        var productIds = await _context.Products.Where(p => listingProductIds.Contains(p.Id) && p.Deleted == false).Select(p => p.Id).ToListAsync();

        if (!productIds.Any()) return;

        await _context.Products.Where(p => productIds.Contains(p.Id)).ExecuteUpdateAsync(s => s.SetProperty(p => p.Deleted, true));

        await _context.ProductImages.Where(img => productIds.Contains(img.ProductId!.Value)).ExecuteUpdateAsync(s => s.SetProperty(img => img.Deleted, true));
    }


}