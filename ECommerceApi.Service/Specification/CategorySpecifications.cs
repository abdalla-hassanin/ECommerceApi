using ECommerceApi.Data.Entities;
using ECommerceApi.Infrastructure.Base.Specifications;

namespace ECommerceApi.Service.Specification;

public static class CategorySpecifications
{
    public sealed class ByParentId(string? parentId) : BaseSpecification<Category>(c => c.ParentCategoryId == parentId);

    public sealed class ActiveOnly() : BaseSpecification<Category>(c => c.IsActive);
    
}
