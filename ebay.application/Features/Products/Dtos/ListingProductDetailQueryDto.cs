using Microsoft.AspNetCore.Mvc;

public class ListingProductDetailQueryDto
{
    [FromQuery(Name = "name")]
    public string? Name { get; set; }

    [FromQuery(Name = "category")]
    public string? CategoryId { get; set; }

    [FromQuery(Name = "price_min")]
    public string? PriceMin { get; set; }

    [FromQuery(Name = "price_max")]
    public string? PriceMax { get; set; }

    [FromQuery(Name = "rating_filter")]
    public string? RatingFilter { get; set; }

    [FromQuery(Name = "order")]
    public string? Order { get; set; }

    [FromQuery(Name = "sort_by")]
    public string? SortBy { get; set; }
}