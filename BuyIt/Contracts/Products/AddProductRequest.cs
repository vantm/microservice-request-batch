namespace Contracts.Products;

public interface AddProductRequest
{
    string Name { get; }
    decimal Price { get; }
}
