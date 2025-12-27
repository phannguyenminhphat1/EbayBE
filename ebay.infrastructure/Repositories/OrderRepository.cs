using System.Text.Json;
using AutoMapper;
using ebay.domain.Entities;
using ebay.infrastructure.Data;
using ebay.infrastructure.Models;
using Microsoft.EntityFrameworkCore;

public class OrderRepository : IOrderRepository
{
    private readonly EBayDbContext _context;
    private readonly IMapper _mapper;

    public OrderRepository(EBayDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<OrderEntity?> GetOrderInCartById(int buyerId)
    {
        var order = await _context.Orders.Include(o => o.OrderDetails).SingleAsync(x => x.BuyerId == buyerId && x.Status == "InCart" && x.Deleted == false);
        if (order == null) return null;
        var orderMapper = _mapper.Map<OrderEntity>(order);
        return orderMapper;
    }

    public async Task<OrderEntity?> GetByOrderDetailIds(List<int> detailIds, int buyerId)
    {
        var distinctIds = detailIds.Distinct().ToList();

        var order = await _context.Orders
            .Include(o => o.OrderDetails)
            .Where(o => o.BuyerId == buyerId && o.Status == "InCart" && o.Deleted == false && o.OrderDetails.Count(d => detailIds.Contains(d.Id) && d.Deleted == false) == distinctIds.Count).SingleOrDefaultAsync();

        return order == null ? null : _mapper.Map<OrderEntity>(order);
    }

    public async Task Add(OrderEntity orderEntity)
    {
        var order = new Order
        {
            BuyerId = orderEntity.BuyerId,
            Status = orderEntity.Status,
            Deleted = false,
            CreatedAt = orderEntity.CreatedAt,
            TotalAmount = orderEntity.TotalAmount
        };
        foreach (var item in orderEntity.OrderDetails)
        {
            order.OrderDetails.Add(new OrderDetail
            {
                Deleted = false,
                ProductId = item.ProductId,
                ListingId = item.ListingId,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
            });
        }
        await _context.Orders.AddAsync(order);
    }

    public async Task Update(OrderEntity orderEntity)
    {
        var order = await _context.Orders.Include(o => o.OrderDetails).SingleAsync(o => o.Id == orderEntity.Id);

        order.TotalAmount = orderEntity.TotalAmount;
        order.Status = orderEntity.Status;

        foreach (var item in orderEntity.OrderDetails)
        {
            if (item.Id == 0)
            {
                order.OrderDetails.Add(new OrderDetail
                {
                    ListingId = item.ListingId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    Deleted = false
                });
            }
            else
            {
                var detail = order.OrderDetails.Single(d => d.Id == item.Id);
                detail.Quantity = item.Quantity;
                detail.Deleted = item.Deleted;
            }
        }
    }


}
