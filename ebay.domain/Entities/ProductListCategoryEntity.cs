using System.Text.Json.Serialization;
namespace ebay.domain.Entities;

public class ProductListCategoryEntity
{
    public int ProductId { get; private set; }

    public string? ProductName { get; private set; }

    public int CategoryId { get; private set; }

    public decimal? Price { get; private set; }

    public string? ProductImage { get; private set; }


}