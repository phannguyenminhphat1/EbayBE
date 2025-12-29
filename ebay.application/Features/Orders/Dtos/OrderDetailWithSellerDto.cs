public class OrderDetailWithSellerDto
{
    public int Id { get; set; }
    public int ListingId { get; set; }
    public int SellerId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}