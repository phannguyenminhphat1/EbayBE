using Microsoft.AspNetCore.Mvc;

public class PaginationDto
{
    [FromQuery(Name = "page")]
    public string? Page { get; set; }

    [FromQuery(Name = "page_size")]
    public string? PageSize { get; set; }
}
