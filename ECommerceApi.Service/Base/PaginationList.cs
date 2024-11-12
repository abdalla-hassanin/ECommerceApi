namespace ECommerceApi.Service.Base;

public sealed record PaginationList<T>
{
    public required IReadOnlyList<T> Items { get; init; }
    public required int CurrentPage { get; init; }
    public required int TotalPages { get; init; }
    public required int PageSize { get; init; }
    public required int TotalCount { get; init; }
}
