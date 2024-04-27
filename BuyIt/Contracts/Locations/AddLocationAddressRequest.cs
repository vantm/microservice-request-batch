namespace Contracts.Locations;

public interface AddLocationAddressRequest
{
    string AddressText { get; }
    string City { get; }
    string District { get; }
}

