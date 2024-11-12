using ECommerceApi.Data.Entities;

namespace ECommerceApi.Service.IService;

public interface IAddressService
{
    Task<Address?> GetAddressByIdAsync(string addressId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Address>> GetAddressesByCustomerIdAsync(string customerId, CancellationToken cancellationToken = default);
    Task<Address> CreateAddressAsync(Address address, CancellationToken cancellationToken = default);
    Task<Address> UpdateAddressAsync(Address address, CancellationToken cancellationToken = default);
    Task DeleteAddressAsync(string addressId, CancellationToken cancellationToken = default);
}