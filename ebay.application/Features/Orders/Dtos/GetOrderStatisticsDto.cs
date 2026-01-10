using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

public class GetOrderStatisticsDto
{
    [JsonPropertyName("label")]
    public int Label { get; set; }

    [JsonPropertyName("total_orders")]
    public int TotalOrders { get; set; }

    [JsonPropertyName("cancelled_orders")]
    public int CancelledOrders { get; set; }

    [JsonPropertyName("pending_orders")]
    public int PendingOrders { get; set; }

    [JsonPropertyName("completed_orders")]
    public int CompletedOrders { get; set; }

    [JsonPropertyName("completed_amount")]
    public decimal CompletedAmount { get; set; }

    [JsonPropertyName("cancelled_amount")]
    public decimal CancelledAmount { get; set; }
}