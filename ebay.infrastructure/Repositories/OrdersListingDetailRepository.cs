
using AutoMapper;
using ebay.application.ReadModels;
using ebay.infrastructure.Data;
using Microsoft.EntityFrameworkCore;
public class OrdersListingDetailRepository : ebay.application.Interfaces.IOrdersListingDetailRepository
{
    private readonly EBayDbContext _context;
    private readonly IMapper _mapper;


    public OrdersListingDetailRepository(EBayDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<(IEnumerable<OrdersListingDetailReadModel> Orders, int TotalRecords)> GetOrdersListingDetail(int buyerId, string? status, int? page, int? pageSize)
    {
        page ??= 1;
        pageSize ??= 10;
        var query = _context.GetOrdersListingDetails.FromSqlRaw("SELECT * FROM GetOrdersListingDetail").AsNoTracking().Where(x => x.BuyerId == buyerId).AsQueryable();
        if (!string.IsNullOrEmpty(status) && status != OrderStatusEnum.All.ToString())
        {
            query = query.Where(x => x.OrderStatus == status);
        }
        var totalRecords = await query.CountAsync();
        var orders = await query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value).ToListAsync();
        var ordersMapper = _mapper.Map<List<OrdersListingDetailReadModel>>(orders);
        return (ordersMapper, totalRecords);
    }

}