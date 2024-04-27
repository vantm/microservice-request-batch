namespace Contracts.Products;

public interface Product
{
    Guid Id { get; }
    string Name { get; }
    decimal Price { get; }
    bool IsEnabled { get; }
}
