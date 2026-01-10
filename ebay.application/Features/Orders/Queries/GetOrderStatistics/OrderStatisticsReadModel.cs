public class OrderStatisticsReadModel
{
    public int Label { get; set; }
    public int TotalOrders { get; set; }

    public int CancelledOrders { get; set; }
    public int PendingOrders { get; set; }

    public int CompletedOrders { get; set; }

    public decimal CompletedAmount { get; set; }
    public decimal CancelledAmount { get; set; }
}
