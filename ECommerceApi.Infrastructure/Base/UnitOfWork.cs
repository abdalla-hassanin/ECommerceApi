using System.Collections.Concurrent;
using ECommerceApi.Infrastructure.Context;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Infrastructure.Base;

public sealed class UnitOfWork(ApplicationDbContext context, ILogger<UnitOfWork> logger, ILoggerFactory loggerFactory) : IUnitOfWork
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));
    private readonly ConcurrentDictionary<Type, object> _repositories = new();
    private readonly ILogger<UnitOfWork> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly ILoggerFactory _loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
    private IDbContextTransaction? _currentTransaction;
    private bool _disposed;

    public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
    {
        return (IGenericRepository<TEntity>)_repositories.GetOrAdd(
            typeof(TEntity),
            _ => new GenericRepository<TEntity>(_context, _loggerFactory.CreateLogger<GenericRepository<TEntity>>()));
    }

    public async Task<int> CompleteAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Saving changes to database");
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_currentTransaction is not null)
        {
            _logger.LogDebug("Transaction already in progress");
            return;
        }

        _logger.LogInformation("Beginning new database transaction");
        _currentTransaction = await _context.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Committing database transaction");
            await _context.SaveChangesAsync(cancellationToken);

            if (_currentTransaction is not null)
            {
                await _currentTransaction.CommitAsync(cancellationToken);
                _logger.LogInformation("Database transaction committed successfully");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while committing transaction. Rolling back.");
            await RollbackTransactionAsync(cancellationToken);
            throw;
        }
        finally
        {
            if (_currentTransaction is not null)
            {
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }
        }
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            if (_currentTransaction is not null)
            {
                _logger.LogInformation("Rolling back database transaction");
                await _currentTransaction.RollbackAsync(cancellationToken);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while rolling back transaction");
        }
        finally
        {
            if (_currentTransaction is not null)
            {
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }
        }
    }

    public void Dispose()
    {
        if (_disposed) return;
        _logger.LogInformation("Disposing UnitOfWork");
        _context.Dispose();
        _disposed = true;
    }
}