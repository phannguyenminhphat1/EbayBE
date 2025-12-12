using System.Text.Json;
using AutoMapper;
using ebay.domain.Entities;
using ebay.infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class ListingProductDetailRepository(IMapper _mapper, EBayDbContext _context) : IListingProductDetailRepository
{
    public async Task<ListingProductDetailEntity?> GetListingProductDetailById(int id)
    {
        var listingProductDetail = await _context.ListingProductDetails.SingleOrDefaultAsync(x => x.ProductId == id && x.Status == "Active" && x.Deleted == false);
        var listingProductDetailMapper = _mapper.Map<ListingProductDetailEntity>(listingProductDetail);
        return listingProductDetailMapper;
    }

    public async Task<(IEnumerable<ListingProductDetailEntity> ListListingProductDetail, int TotalRecords)>
        GetListListingProductDetail(
            int? page,
            int? pageSize = 10,
            string? name = null,
            string? categoryId = null,
            string? priceMin = null,
            string? priceMax = null,
            string? ratingFilter = null,
            string? order = null,
            string? sortBy = null)
    {
        var query = _context.ListingProductDetails
            .Where(l => l.Status == "Active" && l.Deleted == false)
            .AsQueryable();

        // CATEGORY FILTER
        if (!string.IsNullOrWhiteSpace(categoryId) && int.TryParse(categoryId, out var cateId))
        {
            query = query.Where(x => x.CategoryId == cateId);
        }

        // NAME FILTER
        if (!string.IsNullOrWhiteSpace(name))
        {
            var key = name.Trim().ToLower();
            query = query.Where(x => x.Name!.ToLower().Contains(key));
        }

        // PRICE FILTER
        if (decimal.TryParse(priceMin, out var minPrice))
        {
            query = query.Where(x => x.StartingPrice >= minPrice);
        }

        if (decimal.TryParse(priceMax, out var maxPrice))
        {
            query = query.Where(x => x.StartingPrice <= maxPrice);
        }

        // RATING FILTER
        if (int.TryParse(ratingFilter, out var rating))
        {
            query = query.Where(x => x.AverageRatingScore >= rating);
        }

        // SORTING
        bool isDesc = string.IsNullOrWhiteSpace(order) || order.ToLower() == "desc";

        query = sortBy?.ToLower() switch
        {
            "price" => isDesc ? query.OrderByDescending(x => x.StartingPrice) : query.OrderBy(x => x.StartingPrice),
            "sold" => isDesc ? query.OrderByDescending(x => x.TotalSold) : query.OrderBy(x => x.TotalSold),
            "created_at" => isDesc ? query.OrderByDescending(x => x.CreatedAt) : query.OrderBy(x => x.CreatedAt),
            _ => isDesc ? query.OrderByDescending(x => x.CreatedAt) : query.OrderBy(x => x.CreatedAt)
        };

        var totalRecord = await query.CountAsync();

        // PAGINATION
        page ??= 1;
        pageSize ??= 10;

        var lstRaw = await query
            .Skip((page.Value - 1) * pageSize.Value)
            .Take(pageSize.Value)
            .ToListAsync();

        // MAP TO DOMAIN ENTITY
        var lstMapping = _mapper.Map<List<ListingProductDetailEntity>>(lstRaw);

        return (lstMapping, totalRecord);
    }
}
