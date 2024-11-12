using System.Net;
using AutoMapper;
using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.Product.Queries;

public record GetActiveProductsQuery(
    string? OrderBy,
    bool IsDescending,
    int CurrentPage,
    int PageSize
) : IRequest<ApiResponse<IEnumerable<ProductDto>>>;

public class GetActiveProductsQueryValidator : AbstractValidator<GetActiveProductsQuery>
{
    public GetActiveProductsQueryValidator()
    {
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

public class GetActiveProductsHandler(
    IProductService productService,
    IMapper mapper,
    ILogger<GetActiveProductsHandler> logger) : IRequestHandler<GetActiveProductsQuery, ApiResponse<IEnumerable<ProductDto>>>
{
    public async Task<ApiResponse<IEnumerable<ProductDto>>> Handle(GetActiveProductsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting active products (Page: {CurrentPage}, PageSize: {PageSize})", request.CurrentPage, request.PageSize);
        try
        {
            var products = await productService.GetActiveProductsAsync(
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

            logger.LogInformation("Retrieved {Count} active products", products.Items.Count);
            return ApiResponse<IEnumerable<ProductDto>>.CreateResponse(
                HttpStatusCode.OK,
                productDtos,
                "Active products retrieved successfully",
                pagination: paginationMetadata
            );
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while getting active products");
            throw;
        }
    }
}
