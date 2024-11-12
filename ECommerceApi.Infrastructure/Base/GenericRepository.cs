using ECommerceApi.Infrastructure.Base.Specifications;
using ECommerceApi.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Infrastructure.Base;

public class GenericRepository<T>(ApplicationDbContext context, ILogger<GenericRepository<T>> logger) : IGenericRepository<T>
    where T : class
{
    private readonly DbContext _context = context ?? throw new ArgumentNullException(nameof(context));
    private readonly DbSet<T> _dbSet = context.Set<T>();
    private readonly ILogger<GenericRepository<T>> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task<T?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Getting entity of type {EntityType} with id {EntityId}", typeof(T).Name, id);
        return await _dbSet.FindAsync([id], cancellationToken);
    }

    public async Task<T?> GetEntityWithSpecAsync(ISpecification<T> spec, CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Getting entity of type {EntityType} with specification {SpecificationType}", typeof(T).Name, spec.GetType().Name);
        return await ApplySpecification(spec).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<T>> ListAllAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Listing all entities of type {EntityType}", typeof(T).Name);
        return await _dbSet.ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec, CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Listing entities of type {EntityType} with specification {SpecificationType}", typeof(T).Name, spec.GetType().Name);
        return await ApplySpecification(spec).ToListAsync(cancellationToken);
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Adding new entity of type {EntityType}", typeof(T).Name);
        await _dbSet.AddAsync(entity, cancellationToken);
    }

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating entity of type {EntityType}", typeof(T).Name);
        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
        await Task.CompletedTask;
    }

    public async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting entity of type {EntityType}", typeof(T).Name);
        _dbSet.Remove(entity);
        await Task.CompletedTask;
    }

    private IQueryable<T> ApplySpecification(ISpecification<T> spec)
    {
        return SpecificationEvaluator<T>.GetQuery(_dbSet.AsQueryable(), spec);
    }
}