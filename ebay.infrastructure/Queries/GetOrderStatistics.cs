using AutoMapper;
using ebay.infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

public class GetOrderStatistics : IGetOrderStatistics
{
    private readonly EBayDbContext _context;
    private readonly IMapper _mapper;

    public GetOrderStatistics(EBayDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    async Task<List<OrderStatisticsReadModel>> IGetOrderStatistics.GetOrderStatistics(int year, int? month, string groupBy)
    {
        var parameters = new List<SqlParameter>
        {
            new SqlParameter("@Year", year),
            new SqlParameter("@GroupBy", groupBy)
        };

        if (month.HasValue)
        {
            parameters.Add(new SqlParameter("@Month", month.Value));
        }
        else
        {
            parameters.Add(new SqlParameter("@Month", DBNull.Value));
        }

        var result = await _context.Database.SqlQueryRaw<OrderStatisticsReadModel>(
            "EXEC GetOrderStatistics @Year, @Month, @GroupBy",
            parameters.ToArray()
        ).AsNoTracking().ToListAsync();

        return result;

    }
}