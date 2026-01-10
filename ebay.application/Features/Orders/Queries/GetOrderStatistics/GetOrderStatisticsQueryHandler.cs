using System.Net;
using AutoMapper;
using ebay.application.Features.Orders;
using MediatR;

public class GetOrderStatisticsQueryHandler : IRequestHandler<GetOrderStatisticsQuery, ResponseService<List<GetOrderStatisticsDto>>>
{

    private readonly IGetOrderStatistics _orderStatistics;
    private readonly IMapper _mapper;

    public GetOrderStatisticsQueryHandler(IGetOrderStatistics orderStatistics, IMapper mapper)
    {
        _orderStatistics = orderStatistics;
        _mapper = mapper;
    }
    public async Task<ResponseService<List<GetOrderStatisticsDto>>> Handle(GetOrderStatisticsQuery request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        if (string.IsNullOrEmpty(dto.Year))
        {
            return new ResponseService<List<GetOrderStatisticsDto>>(
                statusCode: (int)HttpStatusCode.BadRequest,
                message: OrderMessages.YEAR_IS_REQUIRED
            );
        }
        if (!int.TryParse(dto.Year, out var year) || int.IsNegative(year))
        {
            return new ResponseService<List<GetOrderStatisticsDto>>(
                (int)HttpStatusCode.BadRequest,
                OrderMessages.YEAR_MUST_BE_A_NUMBER
            );
        }
        int? month = null;
        if (!string.IsNullOrEmpty(dto.Month))
        {
            if (!int.TryParse(dto.Month, out var parsedMonth))
            {
                return new ResponseService<List<GetOrderStatisticsDto>>(
                    (int)HttpStatusCode.BadRequest,
                    OrderMessages.MONTH_MUST_BE_A_NUMBER
                );
            }

            if (parsedMonth < 1 || parsedMonth > 12)
            {
                return new ResponseService<List<GetOrderStatisticsDto>>(
                    (int)HttpStatusCode.BadRequest,
                    OrderMessages.MONTH_MUST_BE_BETWEEN_1_AND_12
                );
            }

            month = parsedMonth;
        }
        System.Console.WriteLine(month);
        var groupBy = month.HasValue ? "DAY" : "MONTH";
        var data = await _orderStatistics.GetOrderStatistics(year, month, groupBy);
        var dataMapper = _mapper.Map<List<GetOrderStatisticsDto>>(data);
        return new ResponseService<List<GetOrderStatisticsDto>>(
            statusCode: (int)HttpStatusCode.OK,
            message: OrderMessages.GET_ORDER_STATISTIC_SUCCESSFULLY,
            data: dataMapper
        );
    }
}