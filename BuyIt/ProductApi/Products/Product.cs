namespace ProductApi.Products;

public class Product : Contracts.Products.Product
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public bool IsEnabled { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
