using System.Linq.Expressions;
using ECommerceApi.Data.Entities;
using ECommerceApi.Infrastructure.Base.Specifications;

namespace ECommerceApi.Service.Specification;

public static class ProductSpecifications
{
    public sealed class SearchCount(string? searchTerm, decimal? minPrice, decimal? maxPrice)
        : BaseSpecification<Product>(CreateSearchCriteria(searchTerm, minPrice, maxPrice))
    {
        private static Expression<Func<Product, bool>> CreateSearchCriteria(string? searchTerm, decimal? minPrice, decimal? maxPrice)
        {
            return p => (string.IsNullOrEmpty(searchTerm) || 
                         p.Name.ToLower().Contains(searchTerm.ToLower()) ||
                         p.Description.ToLower().Contains(searchTerm.ToLower()) ||
                         p.ShortDescription.ToLower().Contains(searchTerm.ToLower()))
                        && (!minPrice.HasValue || p.Price >= minPrice.Value)
                        && (!maxPrice.HasValue || p.Price <= maxPrice.Value);
        }
    }
    public sealed class ByCategoryCount(string categoryId)
        : BaseSpecification<Product>(p => p.ProductCategories.Any(pc => pc.CategoryId == categoryId));

    public sealed class ActiveOnlyCount() : BaseSpecification<Product>(p => p.Status == "Active");
    public sealed class Search : BaseSpecification<Product>
    {
        public Search(string? searchTerm, decimal? minPrice, decimal? maxPrice, string? orderBy, bool isDescending, int currentPage, int pageSize)
            : base(CreateSearchCriteria(searchTerm, minPrice, maxPrice))
        {
            ApplyPagingAndOrdering(orderBy, isDescending, currentPage, pageSize);
            AddCommonIncludes();
        }

        private static Expression<Func<Product, bool>> CreateSearchCriteria(string? searchTerm, decimal? minPrice, decimal? maxPrice)
        {
            return p => (string.IsNullOrEmpty(searchTerm) || 
                         p.Name.ToLower().Contains(searchTerm.ToLower()) ||
                         p.Description.ToLower().Contains(searchTerm.ToLower()) ||
                         p.ShortDescription.ToLower().Contains(searchTerm.ToLower()))
                        && (!minPrice.HasValue || p.Price >= minPrice.Value)
                        && (!maxPrice.HasValue || p.Price <= maxPrice.Value);
        }

        private void ApplyPagingAndOrdering(string? orderBy, bool isDescending, int currentPage, int pageSize)
        {
            ApplyPaging((currentPage - 1) * pageSize, pageSize);

            Expression<Func<Product, object>> orderExpression = orderBy?.ToLower() switch
            {
                "name" => p => p.Name,
                "price" => p => p.Price,
                "createdat" => p => p.CreatedAt,
                _ => p => p.ProductId
            };

            if (isDescending)
                ApplyOrderByDescending(orderExpression);
            else
                ApplyOrderBy(orderExpression);
        }

        private void AddCommonIncludes()
        {
            AddInclude(p => p.ProductCategories);
            AddInclude(p => p.Variants);
            AddInclude(p => p.Images);
            AddInclude(p => p.Reviews);
        }
    }

    public sealed class ByCategory : BaseSpecification<Product>
    {
        public ByCategory(string categoryId, string? orderBy, bool isDescending, int currentPage, int pageSize)
            : base(p => p.ProductCategories.Any(pc => pc.CategoryId == categoryId))
        {
            ApplyPagingAndOrdering(orderBy, isDescending, currentPage, pageSize);
            AddCommonIncludes();
        }

        private void ApplyPagingAndOrdering(string? orderBy, bool isDescending, int currentPage, int pageSize)
        {
            ApplyPaging((currentPage - 1) * pageSize, pageSize);

            Expression<Func<Product, object>> orderExpression = orderBy?.ToLower() switch
            {
                "name" => p => p.Name,
                "price" => p => p.Price,
                "createdat" => p => p.CreatedAt,
                _ => p => p.ProductId
            };

            if (isDescending)
                ApplyOrderByDescending(orderExpression);
            else
                ApplyOrderBy(orderExpression);
        }

        private void AddCommonIncludes()
        {
            AddInclude(p => p.ProductCategories);
            AddInclude(p => p.Variants);
            AddInclude(p => p.Images);
            AddInclude(p => p.Reviews);
        }
    }

    public sealed class ActiveOnly : BaseSpecification<Product>
    {
        public ActiveOnly(string? orderBy, bool isDescending, int currentPage, int pageSize)
            : base(p => p.Status == "Active")
        {
            ApplyPagingAndOrdering(orderBy, isDescending, currentPage, pageSize);
            AddCommonIncludes();
        }

        private void ApplyPagingAndOrdering(string? orderBy, bool isDescending, int currentPage, int pageSize)
        {
            ApplyPaging((currentPage - 1) * pageSize, pageSize);

            Expression<Func<Product, object>> orderExpression = orderBy?.ToLower() switch
            {
                "name" => p => p.Name,
                "price" => p => p.Price,
                "createdat" => p => p.CreatedAt,
                _ => p => p.ProductId
            };

            if (isDescending)
                ApplyOrderByDescending(orderExpression);
            else
                ApplyOrderBy(orderExpression);
        }

        private void AddCommonIncludes()
        {
            AddInclude(p => p.ProductCategories);
            AddInclude(p => p.Variants);
            AddInclude(p => p.Images);
            AddInclude(p => p.Reviews);
        }
    }
}