namespace ebay.domain.Entities;

public class ListingEntity
{
    public int Id { get; private set; }
    public int SellerId { get; private set; }
    public int? CategoryId { get; private set; }
    public int? ProductId { get; private set; }

    public string Title { get; private set; } = null!;
    public string? Description { get; private set; }

    public decimal? StartingPrice { get; private set; }
    public decimal? CurrentPrice { get; private set; }

    public bool? IsAuction { get; private set; }
    public DateTime? EndDate { get; private set; }

    public string? Status { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public bool? Deleted { get; private set; }

    // Child entities thuộc Aggregate Listing
    private readonly List<BidEntity> _bids = new();
    public IReadOnlyCollection<BidEntity> Bids => _bids;

    public ListingEntity(
        int sellerId,
        int? categoryId,
        string title,
        string? description,
        decimal? startingPrice,
        bool? isAuction,
        DateTime? endDate,
        int? productId
    )
    {
        SellerId = sellerId;
        CategoryId = categoryId;
        ProductId = productId;
        Title = title;
        Description = description;
        StartingPrice = startingPrice;
        CurrentPrice = startingPrice;
        IsAuction = isAuction;
        EndDate = endDate;
        Status = "Active";
        CreatedAt = DateTime.Now;
        Deleted = false;
    }

    public void UpdateCategory(int? categoryId)
    {
        CategoryId = categoryId;
    }

    public void UpdatePrice(decimal newPrice)
    {
        CurrentPrice = newPrice;
    }

    public void UpdateStatus(string status)
    {
        Status = status;
    }

    public void AddBid(int bidderId, decimal amount)
    {
        _bids.Add(new BidEntity(bidderId, amount, DateTime.Now));
        CurrentPrice = amount;
    }

    public void SoftDelete()
    {
        Deleted = true;
        Status = "Deleted";
    }
}
