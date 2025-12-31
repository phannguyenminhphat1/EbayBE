using Microsoft.AspNetCore.Mvc;

public class GetListingsQueryDto
{
    [FromQuery(Name = "status")]
    public string? Status { get; set; }

    [FromQuery(Name = "name")]
    public string? Name { get; set; }

    [FromQuery(Name = "order")]
    public string? Order { get; set; }
}