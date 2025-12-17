using Microsoft.AspNetCore.Mvc;

public class GetOrdersQueryDto
{
    [FromQuery(Name = "status")]
    public string? Status { get; set; }
}