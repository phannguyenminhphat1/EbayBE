namespace ebay.domain.Entities;

public class ProductEntity
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public decimal? Price { get; set; }

    public int? Stock { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? Deleted { get; set; }

    public ProductEntity(string name, decimal price, int stock)
    {
        Name = name;
        Price = price;
        Stock = stock;
        Deleted = false;
    }

    public void ReduceStock(int quantity)
    {
        if (quantity > Stock)
            throw new InvalidOperationException("Not enough stock");
        Stock -= quantity;
    }

    public void SoftDelete() => Deleted = true;
}