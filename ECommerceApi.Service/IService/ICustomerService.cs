using ECommerceApi.Data.Entities;
using Microsoft.AspNetCore.Http;

namespace ECommerceApi.Service.IService;

public interface ICustomerService
{
    Task<Customer?> GetCustomerByIdAsync(string customerId, CancellationToken cancellationToken = default);
    Task<Customer?> GetCustomerByUserIdAsync(string userId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Customer>> GetAllCustomersAsync(CancellationToken cancellationToken = default);
    Task<Customer> CreateCustomerAsync(Customer customer,IFormFile file, CancellationToken cancellationToken = default);
    Task<Customer> UpdateCustomerAsync(Customer customer, IFormFile? file,CancellationToken cancellationToken = default);
    Task DeleteCustomerAsync(string customerId, CancellationToken cancellationToken = default);
}
