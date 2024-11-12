using ECommerceApi.Data.Entities;
using ECommerceApi.Infrastructure.Base;
using ECommerceApi.Service.IService;
using ECommerceApi.Service.Specification;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Service.Service;

public class AddressService : IAddressService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AddressService> _logger;

    public AddressService(IUnitOfWork unitOfWork, ILogger<AddressService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Address?> GetAddressByIdAsync(string addressId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Getting address with ID: {AddressId}", addressId);
        return await _unitOfWork.Repository<Address>().GetByIdAsync(addressId, cancellationToken);
    }

    public async Task<IReadOnlyList<Address>> GetAddressesByCustomerIdAsync(string customerId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Getting addresses for customer ID: {CustomerId}", customerId);
        var spec = new AddressSpecifications(a => a.CustomerId == customerId);
        return await _unitOfWork.Repository<Address>().ListAsync(spec, cancellationToken);
    }

    public async Task<Address> CreateAddressAsync(Address address, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Creating new address for customer ID: {CustomerId}", address.CustomerId);
        await _unitOfWork.Repository<Address>().AddAsync(address, cancellationToken);
        await _unitOfWork.CompleteAsync(cancellationToken);
        _logger.LogInformation("Address created with ID: {AddressId}", address.AddressId);
        return address;
    }

    public async Task<Address> UpdateAddressAsync(Address address, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating address with ID: {AddressId}", address.AddressId);
        await _unitOfWork.Repository<Address>().UpdateAsync(address, cancellationToken);
        await _unitOfWork.CompleteAsync(cancellationToken);
        _logger.LogInformation("Address updated successfully");
        return address;
    }

    public async Task DeleteAddressAsync(string addressId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting address with ID: {AddressId}", addressId);
        var address = await GetAddressByIdAsync(addressId, cancellationToken);
        if (address is not null)
        {
            await _unitOfWork.Repository<Address>().DeleteAsync(address, cancellationToken);
            await _unitOfWork.CompleteAsync(cancellationToken);
            _logger.LogInformation("Address deleted successfully");
        }
        else
        {
            _logger.LogWarning("Attempted to delete non-existent address with ID: {AddressId}", addressId);
        }
    }
}