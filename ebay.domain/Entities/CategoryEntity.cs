namespace ebay.domain.Entities;

public class CategoryEntity
{
    public int Id { get; private set; }

    public string Name { get; private set; } = null!;
    public string? Description { get; private set; }
    public bool? Deleted { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public string? Image { get; private set; }


    // Category KHÔNG chứa danh sách listing trong domain
    // Vì listing thuộc aggregate khác

    public CategoryEntity(string name, string? description)
    {
        Name = name;
        Description = description;
        CreatedAt = DateTime.Now;
        Deleted = false;
    }

    public void UpdateName(string newName)
    {
        Name = newName;
    }

    public void UpdateDescription(string? desc)
    {
        Description = desc;
    }

    public void SoftDelete()
    {
        Deleted = true;
    }
}
