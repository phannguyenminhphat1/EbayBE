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
}
