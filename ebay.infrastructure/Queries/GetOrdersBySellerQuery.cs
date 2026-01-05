using AutoMapper;
using ebay.application.Interfaces;
using ebay.infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ebay.infrastructure.Queries;

public class GetOrdersBySellerQuery : IGetOrdersBySellerQuery
{
    private readonly EBayDbContext _context;
    private readonly IMapper _mapper;


    public GetOrdersBySellerQuery(EBayDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;

    }

    public async Task<(IEnumerable<GetOrdersBySellerReadModel> Orders, int TotalRecords)> GetOrdersBySeller(int sellerId, string? status, int? page, int? pageSize)
    {
        page ??= 1;
        pageSize ??= 10;
        var query = _context.GetOrdersBySellers.FromSqlRaw("SELECT * FROM GetOrdersBySeller").AsNoTracking().Where(x => x.SellerId == sellerId).AsQueryable();
        if (!string.IsNullOrEmpty(status) && status != OrderStatusEnum.All.ToString() && status != OrderStatusEnum.InCart.ToString())
        {
            query = query.Where(x => x.OrderStatus == status);
        }
        var totalRecords = await query.CountAsync();
        var orders = await query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value).ToListAsync();
        var ordersMapper = _mapper.Map<List<GetOrdersBySellerReadModel>>(orders);
        return (ordersMapper, totalRecords);
    }
}
