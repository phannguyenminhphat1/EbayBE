namespace ebay.domain.Entities;

public class BidEntity
{
    public int BidderId { get; private set; }
    public decimal Amount { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public BidEntity(int bidderId, decimal amount, DateTime createdAt)
    {
        BidderId = bidderId;
        Amount = amount;
        CreatedAt = createdAt;
    }
}
