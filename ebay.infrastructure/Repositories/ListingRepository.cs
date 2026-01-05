
using AutoMapper;
using ebay.domain.Entities;
using ebay.infrastructure.Data;
using ebay.infrastructure.Models;
using Microsoft.EntityFrameworkCore;

public class ListingRepository(IMapper _mapper, EBayDbContext _context) : IListingRepository
{
    public async Task<ListingEntity?> GetById(int id)
    {
        var listing = await _context.Listings.SingleOrDefaultAsync(x => x.Id == id);
        return listing == null ? null : _mapper.Map<ListingEntity>(listing);
    }

    public async Task<List<ListingEntity>?> GetByIds(List<int> ids, int userId, string userRole)
    {
        if (ids == null || ids.Count == 0) return [];
        var query = _context.Listings.Where(x => ids.Contains(x.Id) && x.Deleted == false);
        if (userRole.Equals(UserRoleEnum.Seller.ToString(), StringComparison.OrdinalIgnoreCase))
        {
            query = query.Where(x => x.SellerId == userId);
        }
        var lstListing = await query.ToListAsync();
        return _mapper.Map<List<ListingEntity>>(lstListing);
    }

    public async Task Update(ListingEntity listingEntity)
    {
        var listing = await _context.Listings.SingleAsync(o => o.Id == listingEntity.Id);
        if (listing == null) return;
        listing.Deleted = listingEntity.Deleted;
        listing.Status = listingEntity.Status;
        _context.Listings.Update(listing);
    }
    public async Task SoftDeleteByIds(List<int> ids, int userId, string userRole)
    {
        var query = _context.Listings.Where(x => ids.Contains(x.Id) && x.Deleted == false);

        if (userRole.Equals(UserRoleEnum.Seller.ToString(), StringComparison.OrdinalIgnoreCase))
        {
            query = query.Where(x => x.SellerId == userId);
        }

        await query.ExecuteUpdateAsync(s => s.SetProperty(x => x.Deleted, true)
        );
    }

    public async Task Add(ListingEntity entity)
    {
        var listing = new Listing
        {
            SellerId = entity.SellerId,
            CategoryId = entity.CategoryId,
            Title = entity.Title,
            Description = entity.Description,
            StartingPrice = entity.StartingPrice,
            CurrentPrice = entity.CurrentPrice,
            IsAuction = entity.IsAuction,
            EndDate = entity.EndDate,
            Status = entity.Status,
            CreatedAt = entity.CreatedAt,
            Deleted = false,
            Product = new Product
            {
                Name = entity.Product!.Name,
                Description = entity.Product.Description,
                Price = entity.Product.Price,
                Stock = entity.Product.Stock,
                CreatedAt = entity.Product.CreatedAt,
                Deleted = false,
                ProductImages = entity.Product.Images.Select(img => new ProductImage
                {
                    ImageUrl = img.ImageUrl,
                    IsPrimary = img.IsPrimary,
                    CreatedAt = img.CreatedAt,
                    Deleted = false
                }).ToList()
            }
        };

        await _context.Listings.AddAsync(listing);
    }

}