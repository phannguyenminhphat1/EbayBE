namespace ebay.domain.Entities;

public class ProductEntity
{
    public int Id { get; private set; }

    public string Name { get; private set; } = null!;
    public string? Description { get; private set; }
    public decimal? Price { get; private set; }
    public int? Stock { get; private set; }
    public bool? Deleted { get; private set; }
    public DateTime? CreatedAt { get; private set; }

    // Product Images
    private readonly List<ProductImageEntity> _images = new();
    public IReadOnlyCollection<ProductImageEntity> Images => _images;

    // Ratings
    private readonly List<RatingEntity> _ratings = new();
    public IReadOnlyCollection<RatingEntity> Ratings => _ratings;

    public ProductEntity(string name, string? description, decimal? price, int? stock)
    {
        Name = name;
        Description = description;
        Price = price;
        Stock = stock;
        CreatedAt = DateTime.Now;
        Deleted = false;
    }

    public void AddImage(string url, bool? isPrimary)
    {
        _images.Add(new ProductImageEntity(url, isPrimary));
    }

    public void AddRating(int raterId, int ratedUserId, int score, string? comment)
    {
        _ratings.Add(new RatingEntity(raterId, ratedUserId, score, comment));
    }

    public void UpdateStock(int quantity)
    {
        Stock = quantity;
    }

    public void SoftDelete()
    {
        Deleted = true;
    }
}
