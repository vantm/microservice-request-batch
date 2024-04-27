namespace LocationApi.Addresses;

public class Address : Contracts.Locations.LocationAddress
{
    public Guid Id { get; set; }
    public string AddressText { get; set; } = "";
    public string District { get; set; } = "";
    public string City { get; set; } = "";
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
