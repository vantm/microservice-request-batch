namespace WarehouseApi.Warehouses;

public class Warehouse : Contracts.Warehouses.Warehouse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public bool IsEnabled { get; set; }
    public Guid AddressId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
