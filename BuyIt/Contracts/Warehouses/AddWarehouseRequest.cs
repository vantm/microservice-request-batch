namespace Contracts.Warehouses;

public interface AddWarehouseRequest
{
    string Name { get; }
    Guid AddressId { get; }
}
