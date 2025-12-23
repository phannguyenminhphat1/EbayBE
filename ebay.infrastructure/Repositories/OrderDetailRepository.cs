using AutoMapper;
using ebay.domain.Entities;
using ebay.infrastructure.Data;
using ebay.infrastructure.Models;
using Microsoft.EntityFrameworkCore;

public class OrderDetailRepository : IOrderDetailRepository
{
    private readonly EBayDbContext _context;
    private readonly IMapper _mapper;

    public OrderDetailRepository(EBayDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<OrderDetailEntity?> GetByOrderIdAndListingId(int orderId, int listingId)
    {
        var detail = await _context.OrderDetails
            .SingleAsync(d =>
                d.OrderId == orderId &&
                d.ListingId == listingId &&
                d.Deleted == false);

        return detail == null ? null : _mapper.Map<OrderDetailEntity>(detail);
    }
    public async Task<OrderDetailEntity?> GetById(int id)
    {
        var orderDetail = await _context.OrderDetails.SingleOrDefaultAsync(x => x.Id == id && x.Deleted == false);
        if (orderDetail == null) return null;
        var orderDetailMapper = _mapper.Map<OrderDetailEntity>(orderDetail);
        return orderDetailMapper;
    }

    public async Task<List<OrderDetailEntity>> GetByIds(List<int> ids)
    {
        if (ids == null || ids.Count == 0) return [];
        var orderDetails = await _context.OrderDetails.Where(x => ids.Contains(x.Id) && x.Deleted == false).ToListAsync();
        return _mapper.Map<List<OrderDetailEntity>>(orderDetails);
    }
}
