using System.Net;
using AutoMapper;
using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.Product.Queries;

public record SearchProductsQuery(
    string? SearchTerm,
    decimal? MinPrice,
    decimal? MaxPrice,
    string? OrderBy,
    bool IsDescending,
    int CurrentPage,
    int PageSize
) : IRequest<ApiResponse<IEnumerable<ProductDto>>>;

public class SearchProductsQueryValidator : AbstractValidator<SearchProductsQuery>
{
    public SearchProductsQueryValidator()
    {
        RuleFor(x => x.MinPrice)
            .GreaterThanOrEqualTo(0).When(x => x.MinPrice.HasValue)
            .WithMessage("Minimum price must be greater than or equal to 0.");
        RuleFor(x => x.MaxPrice)
            .GreaterThanOrEqualTo(0).When(x => x.MaxPrice.HasValue)
            .WithMessage("Maximum price must be greater than or equal to 0.");
        RuleFor(x => x.MaxPrice)
            .GreaterThanOrEqualTo(x => x.MinPrice).When(x => x.MinPrice.HasValue && x.MaxPrice.HasValue)
            .WithMessage("Maximum price must be greater than or equal to minimum price.");
        RuleFor(x => x.CurrentPage)
            .GreaterThan(0)
            .WithMessage("Current page must be greater than 0.");
        RuleFor(x => x.PageSize)
            .GreaterThan(0)
            .WithMessage("Page size must be greater than 0.")
            .LessThanOrEqualTo(100)
            .WithMessage("Page size must be less than or equal to 100.");
    }
}

public class SearchProductsHandler(
    IProductService productService,
    IMapper mapper,
    ILogger<SearchProductsHandler> logger) : IRequestHandler<SearchProductsQuery, ApiResponse<IEnumerable<ProductDto>>>
{
    public async Task<ApiResponse<IEnumerable<ProductDto>>> Handle(SearchProductsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Searching products (SearchTerm: {SearchTerm}, MinPrice: {MinPrice}, MaxPrice: {MaxPrice}, Page: {CurrentPage}, PageSize: {PageSize})", 
            request.SearchTerm, request.MinPrice, request.MaxPrice, request.CurrentPage, request.PageSize);
        try
        {
            var products = await productService.SearchProductsAsync(
                request.SearchTerm, request.MinPrice, request.MaxPrice,
                request.OrderBy, request.IsDescending, request.CurrentPage, request.PageSize,
                cancellationToken);

            var productDtos = mapper.Map<IEnumerable<ProductDto>>(products.Items);

            var paginationMetadata = new PaginationMetadata
            {
                CurrentPage = products.CurrentPage,
                TotalPages = (int)Math.Ceiling(products.TotalCount / (double)request.PageSize),
                PageSize = request.PageSize,
                TotalCount = products.TotalCount
            };

            logger.LogInformation("Retrieved {Count} products matching search criteria", products.Items.Count);
            return ApiResponse<IEnumerable<ProductDto>>.CreateResponse(
                HttpStatusCode.OK,
                productDtos,
                "Products retrieved successfully",
                pagination: paginationMetadata
            );
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while searching products");
            throw;
        }
    }
}
