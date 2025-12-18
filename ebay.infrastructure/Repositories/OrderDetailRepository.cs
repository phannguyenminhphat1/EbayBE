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

    public async Task<OrderDetailEntity?> GetByOrderAndProduct(int orderId, int productId)
    {
        var detail = await _context.OrderDetails
            .SingleOrDefaultAsync(d =>
                d.OrderId == orderId &&
                d.ProductId == productId &&
                d.Deleted == false);

        return detail == null ? null : _mapper.Map<OrderDetailEntity>(detail);
    }

    public async Task Add(OrderDetailEntity detail)
    {
        var model = _mapper.Map<OrderDetail>(detail);
        await _context.OrderDetails.AddAsync(model);
    }

    public async Task DeleteOrderDetails(List<OrderDetailEntity> entities)
    {
        if (entities == null || entities.Count == 0) return;
        var ids = entities.Select(x => x.Id).ToList();
        var orderDetails = await _context.OrderDetails.Where(x => ids.Contains(x.Id) && x.Deleted == false).ToListAsync();
        if (orderDetails.Count == 0) return;
        _context.OrderDetails.RemoveRange(orderDetails);
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
