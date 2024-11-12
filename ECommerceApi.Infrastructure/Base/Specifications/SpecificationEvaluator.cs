using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Infrastructure.Base.Specifications;

public static class SpecificationEvaluator<T> where T : class
{
    private static readonly ILogger Logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger(typeof(SpecificationEvaluator<T>));

    public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> specification)
    {
        ArgumentNullException.ThrowIfNull(inputQuery);
        ArgumentNullException.ThrowIfNull(specification);

        Logger.LogDebug("Evaluating specification for {EntityType}", typeof(T).Name);

        var query = inputQuery;

        if (specification.Criteria is not null)
        {
            query = query.Where(specification.Criteria);
            Logger.LogDebug("Applied criteria to query for {EntityType}", typeof(T).Name);
        }

        query = specification.Includes.Aggregate(query, (current, include) =>
        {
            Logger.LogDebug("Applying include expression to query for {EntityType}", typeof(T).Name);
            return current.Include(include);
        });

        query = specification.IncludeStrings.Aggregate(query, (current, include) =>
        {
            Logger.LogDebug("Applying include string '{IncludeString}' to query for {EntityType}", include, typeof(T).Name);
            return current.Include(include);
        });

        if (specification.OrderBy is not null)
        {
            query = query.OrderBy(specification.OrderBy);
            Logger.LogDebug("Applied OrderBy to query for {EntityType}", typeof(T).Name);
        }
        else if (specification.OrderByDescending is not null)
        {
            query = query.OrderByDescending(specification.OrderByDescending);
            Logger.LogDebug("Applied OrderByDescending to query for {EntityType}", typeof(T).Name);
        }

        if (specification.IsPagingEnabled)
        {
            query = query.Skip(specification.Skip ?? 0).Take(specification.Take ?? 0);
            Logger.LogDebug("Applied paging to query for {EntityType}: Skip {Skip}, Take {Take}", typeof(T).Name, specification.Skip, specification.Take);
        }

        return query;
    }
}