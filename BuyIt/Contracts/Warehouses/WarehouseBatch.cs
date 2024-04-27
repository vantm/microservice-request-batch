namespace Contracts.Warehouses;

public interface WarehouseBatch
{
    Guid Id { get; }
    string Key { get; }
    Guid ProductId { get; }
    Guid WarehouseId { get; }
    int Quantity { get; }
}
