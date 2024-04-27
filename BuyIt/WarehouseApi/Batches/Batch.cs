namespace WarehouseApi.Batches;

public class Batch : Contracts.Warehouses.WarehouseBatch
{
    public Guid Id { get; set; }
    public string Key { get; set; } = "";
    public Guid ProductId { get; set; }
    public Guid WarehouseId { get; set; }
    public int Quantity { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
