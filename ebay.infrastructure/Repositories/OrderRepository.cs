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
        var order = await _context.Orders.Include(o => o.OrderDetails).SingleOrDefaultAsync(x => x.BuyerId == buyerId && x.Status == "InCart" && x.Deleted == false);
        if (order == null) return null;
        var orderMapper = _mapper.Map<OrderEntity>(order);
        return orderMapper;
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
            var detail = order.OrderDetails.SingleOrDefault(x => x.ProductId == item.ProductId && x.Deleted == false);

            if (detail == null)
            {
                order.OrderDetails.Add(new OrderDetail
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    Deleted = false
                });
            }
            else
            {
                detail.Quantity = item.Quantity;
            }
        }
    }


}
