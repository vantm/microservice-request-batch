namespace Contracts.Warehouses;

public interface Warehouse
{
    Guid Id { get; }
    string Name { get; }
    bool IsEnabled { get; }
    Guid AddressId { get; }
}
