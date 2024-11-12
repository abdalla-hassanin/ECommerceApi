using System.Linq.Expressions;
using ECommerceApi.Infrastructure.Base.Specifications;
using Microsoft.EntityFrameworkCore.Query;

namespace ECommerceApi.Infrastructure.Base;

public interface IGenericRepository<T> where T : class
{
    // Queries Operations
    Task<T?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<T?> GetEntityWithSpecAsync(ISpecification<T> spec, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<T>> ListAllAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec, CancellationToken cancellationToken = default);
    
    // Commands Operations
    Task AddAsync(T entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
}