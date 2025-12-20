
using AutoMapper;
using ebay.domain.Entities;
using ebay.infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class ListingRepository(IMapper _mapper, EBayDbContext _context) : IListingRepository
{
    public async Task<ListingEntity?> GetById(int id)
    {
        var listing = await _context.Listings.SingleOrDefaultAsync(x => x.Id == id);
        return listing == null ? null : _mapper.Map<ListingEntity>(listing);
    }
}