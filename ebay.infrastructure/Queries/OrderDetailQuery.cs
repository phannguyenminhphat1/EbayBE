using ebay.infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ebay.infrastructure.Queries;

public class OrderDetailQuery : IOrderDetailQuery
{
    private readonly EBayDbContext _context;

    public OrderDetailQuery(EBayDbContext context)
    {
        _context = context;
    }

    public async Task<List<OrderDetailWithSellerDto>> GetByIdsWithSeller(List<int> ids)
    {
        return await _context.OrderDetails.Where(x => ids.Contains(x.Id) && x.Deleted == false)
            .Select(x => new OrderDetailWithSellerDto
            {
                Id = x.Id,
                ListingId = x.ListingId!.Value,
                SellerId = x.Listing!.SellerId,
                ProductId = x.ProductId,
                Quantity = x.Quantity,
                UnitPrice = x.UnitPrice
            })
            .ToListAsync();
    }
}
