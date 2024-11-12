using System.Linq.Expressions;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Infrastructure.Base.Specifications;

public abstract class BaseSpecification<T> : ISpecification<T>
{
    private static readonly ILogger<BaseSpecification<T>> Logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<BaseSpecification<T>>();

    private readonly List<Expression<Func<T, object>>> _includes = [];
    private readonly List<string> _includeStrings = [];
    public Expression<Func<T, bool>>? Criteria { get; set; } 
    public IReadOnlyList<Expression<Func<T, object>>> Includes => _includes.AsReadOnly();
    public IReadOnlyList<string> IncludeStrings => _includeStrings.AsReadOnly();
    public Expression<Func<T, object>>? OrderBy { get; private set; }
    public Expression<Func<T, object>>? OrderByDescending { get; private set; }
    public int? Take { get; private set; }
    public int? Skip { get; private set; }
    public bool IsPagingEnabled { get; private set; }
    
    protected BaseSpecification() { }

    protected BaseSpecification(Expression<Func<T, bool>>? criteria)
    {
        Criteria = criteria;
    }
    
    protected void AddInclude(Expression<Func<T, object>> includeExpression)
    {
        ArgumentNullException.ThrowIfNull(includeExpression);
        _includes.Add(includeExpression);
        Logger.LogDebug("Added include expression to specification for {EntityType}", typeof(T).Name);
    }

    protected void AddInclude(string includeString)
    {
        ArgumentException.ThrowIfNullOrEmpty(includeString);
        _includeStrings.Add(includeString);
        Logger.LogDebug("Added include string '{IncludeString}' to specification for {EntityType}", includeString, typeof(T).Name);
    }

    protected void ApplyPaging(int skip, int take)
    {
        Skip = skip;
        Take = take;
        IsPagingEnabled = true;
        Logger.LogDebug("Applied paging to specification for {EntityType}: Skip {Skip}, Take {Take}", typeof(T).Name, skip, take);
    }

    protected void ApplyOrderBy(Expression<Func<T, object>> orderByExpression)
    {
        OrderBy = orderByExpression ?? throw new ArgumentNullException(nameof(orderByExpression));
        Logger.LogDebug("Applied OrderBy to specification for {EntityType}", typeof(T).Name);
    }

    protected void ApplyOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression)
    {
        OrderByDescending = orderByDescendingExpression ?? throw new ArgumentNullException(nameof(orderByDescendingExpression));
        Logger.LogDebug("Applied OrderByDescending to specification for {EntityType}", typeof(T).Name);
    }
}