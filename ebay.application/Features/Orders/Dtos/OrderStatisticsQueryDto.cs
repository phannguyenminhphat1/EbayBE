using Microsoft.AspNetCore.Mvc;

public class OrderStatisticsQueryDto
{
    [FromQuery(Name = "year")]
    public string? Year { get; set; }

    [FromQuery(Name = "month")]
    public string? Month { get; set; }
}