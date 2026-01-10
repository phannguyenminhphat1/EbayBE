public interface IGetOrderStatistics
{
    Task<List<OrderStatisticsReadModel>> GetOrderStatistics(int year, int? month, string groupBy);

}
