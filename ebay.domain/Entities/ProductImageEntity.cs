namespace ebay.domain.Entities;

public class ProductImageEntity
{
    public int Id { get; private set; }
    public string ImageUrl { get; private set; } = null!;
    public bool? IsPrimary { get; private set; }
    public bool? Deleted { get; private set; }
    public DateTime? CreatedAt { get; private set; }

    public ProductImageEntity(string url, bool? isPrimary)
    {
        ImageUrl = url;
        IsPrimary = isPrimary;
        CreatedAt = DateTime.Now;
        Deleted = false;
    }

    public void MarkAsPrimary()
    {
        IsPrimary = true;
    }

    public void SoftDelete()
    {
        Deleted = true;
    }
}
