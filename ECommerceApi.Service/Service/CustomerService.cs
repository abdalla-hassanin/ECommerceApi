using ECommerceApi.Data.Entities;
using ECommerceApi.Infrastructure.Base;
using ECommerceApi.Service.IService;
using ECommerceApi.Service.Specification;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Service.Service;

public class CustomerService : ICustomerService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CustomerService> _logger;
    private readonly IAwsStorageService _awsStorageService;

    public CustomerService(IUnitOfWork unitOfWork, ILogger<CustomerService> logger,
        IAwsStorageService awsStorageService)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _awsStorageService = awsStorageService;
    }

    public async Task<Customer?> GetCustomerByIdAsync(string customerId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Getting customer with ID: {CustomerId}", customerId);
        var spec = new CustomerSpecifications.ByCustomerId(customerId);
        var customers = await _unitOfWork.Repository<Customer>().ListAsync(spec, cancellationToken);
        return customers.FirstOrDefault();
    }

    public async Task<Customer?> GetCustomerByUserIdAsync(string userId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Getting customer by user ID: {UserId}", userId);
        var spec = new CustomerSpecifications.ByUserId(userId);
        var customers = await _unitOfWork.Repository<Customer>().ListAsync(spec, cancellationToken);
        return customers.FirstOrDefault();
    }

    public async Task<IReadOnlyList<Customer>> GetAllCustomersAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Getting all customers");
        var spec = new CustomerSpecifications.AllCustomers();
        return await _unitOfWork.Repository<Customer>().ListAsync(spec, cancellationToken);
    }

    public async Task<Customer> CreateCustomerAsync(Customer customer, IFormFile file,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Creating new customer");
        
        // Upload the image to storage
        var prefix = "customer";
        customer.ProfilePictureUrl =
            await _awsStorageService.UploadImageAsync(file, prefix, cancellationToken);
        await _unitOfWork.Repository<Customer>().AddAsync(customer, cancellationToken);
        await _unitOfWork.CompleteAsync(cancellationToken);
        _logger.LogInformation("Customer created with ID: {CustomerId}", customer.CustomerId);
        return customer;
    }

    public async Task<Customer> UpdateCustomerAsync(Customer customer,IFormFile? file,  CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating customer with ID: {CustomerId}", customer.CustomerId);
        if (file is not null)
        {
            // Delete the old image from storage
            await _awsStorageService.DeleteImageAsync(customer.ProfilePictureUrl, cancellationToken);

            // Upload the new image to storage
            var prefix = "customer";
            customer.ProfilePictureUrl = await _awsStorageService.UploadImageAsync(file, prefix, cancellationToken);
        }

        await _unitOfWork.Repository<Customer>().UpdateAsync(customer, cancellationToken);
        await _unitOfWork.CompleteAsync(cancellationToken);
        _logger.LogInformation("Customer updated successfully");
        return customer;
    }

    public async Task DeleteCustomerAsync(string customerId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting customer with ID: {CustomerId}", customerId);
        var customer = await GetCustomerByIdAsync(customerId, cancellationToken);
        if (customer is not null)
        {
            // Delete the image from storage
            await _awsStorageService.DeleteImageAsync(customer.ProfilePictureUrl, cancellationToken);
            
            await _unitOfWork.Repository<Customer>().DeleteAsync(customer, cancellationToken);
            await _unitOfWork.CompleteAsync(cancellationToken);
            _logger.LogInformation("Customer deleted successfully");
        }
        else
        {
            _logger.LogWarning("Attempted to delete non-existent customer with ID: {CustomerId}", customerId);
        }
    }
}