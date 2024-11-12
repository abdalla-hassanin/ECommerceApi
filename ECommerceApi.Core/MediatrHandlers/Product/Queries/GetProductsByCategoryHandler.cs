using System.Net;
using AutoMapper;
using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.Product.Queries;

public record GetProductsByCategoryQuery(
    string CategoryId,
    string? OrderBy,
    bool IsDescending,
    int CurrentPage,
    int PageSize
) : IRequest<ApiResponse<IEnumerable<ProductDto>>>;

public class GetProductsByCategoryQueryValidator : AbstractValidator<GetProductsByCategoryQuery>
{
    public GetProductsByCategoryQueryValidator()
    {
        RuleFor(x => x.CategoryId)
            .NotEmpty()
            .Length(26)
            .WithMessage("Category ID must be a valid ULID.");
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

public class GetProductsByCategoryHandler(
    IProductService productService,
    IMapper mapper,
    ILogger<GetProductsByCategoryHandler> logger) : IRequestHandler<GetProductsByCategoryQuery, ApiResponse<IEnumerable<ProductDto>>>
{
    public async Task<ApiResponse<IEnumerable<ProductDto>>> Handle(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting products for category ID: {CategoryId} (Page: {CurrentPage}, PageSize: {PageSize})", 
            request.CategoryId, request.CurrentPage, request.PageSize);
        try
        {
            var products = await productService.GetProductsByCategoryAsync(
                request.CategoryId, request.OrderBy, request.IsDescending, request.CurrentPage, request.PageSize,
                cancellationToken);

            var productDtos = mapper.Map<IEnumerable<ProductDto>>(products.Items);
            var paginationMetadata = new PaginationMetadata
            {
                CurrentPage = products.CurrentPage,
                TotalPages = (int)Math.Ceiling(products.TotalCount / (double)request.PageSize),
                PageSize = request.PageSize,
                TotalCount = products.TotalCount
            };

            logger.LogInformation("Retrieved {Count} products for category ID: {CategoryId}", products.Items.Count, request.CategoryId);
            return ApiResponse<IEnumerable<ProductDto>>.CreateResponse(
                HttpStatusCode.OK,
                productDtos,
                "Products retrieved successfully",
                pagination: paginationMetadata
            );
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while getting products for category ID: {CategoryId}", request.CategoryId);
            throw;
        }
    }
}
