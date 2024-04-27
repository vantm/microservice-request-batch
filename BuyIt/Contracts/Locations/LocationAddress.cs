namespace Contracts.Locations;

public interface LocationAddress
{
    Guid Id { get; }
    string AddressText { get; }
    string District { get; }
    string City { get; }
}
