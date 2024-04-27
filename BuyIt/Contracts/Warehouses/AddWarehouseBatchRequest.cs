namespace Contracts.Warehouses;

public interface AddWarehouseBatchRequest
{
    string Key { get; }
    Guid ProductId { get; }
    Guid WarehouseId { get; }
    int Quantity { get; }
}
