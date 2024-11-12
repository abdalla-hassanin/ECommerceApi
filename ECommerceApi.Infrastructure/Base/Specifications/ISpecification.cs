using System.Linq.Expressions;

namespace ECommerceApi.Infrastructure.Base.Specifications;

public interface ISpecification<T>
{
    Expression<Func<T, bool>>? Criteria { get; }
    IReadOnlyList<Expression<Func<T, object>>> Includes { get; }
    IReadOnlyList<string> IncludeStrings { get; }
    Expression<Func<T, object>>? OrderBy { get; }
    Expression<Func<T, object>>? OrderByDescending { get; }
    int? Take { get; }
    int? Skip { get; }
    bool IsPagingEnabled { get; }
}
