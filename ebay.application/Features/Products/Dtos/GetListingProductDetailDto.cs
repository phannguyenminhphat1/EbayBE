
using System.Text.Json.Serialization;

public class GetListingProductDetailDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime? CreatedAt { get; set; }

    [JsonPropertyName("deleted")]
    public bool? Deleted { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("category_id")]
    public int? CategoryId { get; set; }

    [JsonPropertyName("category_name")]
    public string CategoryName { get; set; } = null!;

    [JsonPropertyName("user_id")]
    public int UserId { get; set; }

    [JsonPropertyName("fullname")]
    public string? FullName { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; } = null!;

    [JsonPropertyName("phone")]
    public string? Phone { get; set; }

    [JsonPropertyName("address")]
    public string? Address { get; set; }

    [JsonPropertyName("avatar")]
    public string Avatar { get; set; } = null!;

    [JsonPropertyName("product_id")]
    public int ProductId { get; set; }

    [JsonPropertyName("product_name")]
    public string? Name { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("starting_price")]
    public decimal? StartingPrice { get; set; }

    [JsonPropertyName("current_price")]
    public decimal? CurrentPrice { get; set; }

    [JsonPropertyName("list_image_detail")]
    public string? ListImageDetail { get; set; }

    [JsonPropertyName("group_listing")]
    public int? GroupListing { get; set; }

    [JsonPropertyName("total_sold")]
    public int? TotalSold { get; set; }
    [JsonPropertyName("average_rating_score")]
    public int? AverageRatingScore { get; set; }
}