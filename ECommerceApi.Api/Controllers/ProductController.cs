using ECommerceApi.Api.ResponseExample;
using ECommerceApi.Core.Base.Response;
using ECommerceApi.Core.MediatrHandlers.Product;
using ECommerceApi.Core.MediatrHandlers.Product.Commands;
using ECommerceApi.Core.MediatrHandlers.Product.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace ECommerceApi.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController(IMediator mediator) : ControllerBase
{
    [HttpGet("{productId}")]
    [SwaggerOperation(
        Summary = "Get Product by ID",
        Description = "This endpoint allows Admins, Customers and public users to retrieve a product by its ID."
    )]
    [ProducesResponseType(typeof(ApiResponse<ProductDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<ProductDto>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<ProductDto>), StatusCodes.Status401Unauthorized)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetProductByIdResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(NotFoundProductResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedProductResponseExample))]
    public async Task<IResult> GetProductById(
        string productId,
        CancellationToken cancellationToken)
    {
        var query = new GetProductByIdQuery(productId);
        var result = await mediator.Send(query, cancellationToken);
        return result.ToResult();
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get All Products",
        Description = "This endpoint allows Admins, Customers and public users to retrieve all products."
    )]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<ProductDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<ProductDto>>), StatusCodes.Status401Unauthorized)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetAllProductsResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedProductResponseExample))]
    public async Task<IResult> GetAllProducts(CancellationToken cancellationToken)
    {
        var query = new GetAllProductsQuery();
        var result = await mediator.Send(query, cancellationToken);
        return result.ToResult();
    }

    [HttpGet("search")]
    [SwaggerOperation(
        Summary = "Search Products",
        Description = "This endpoint allows Admins, Customers and public users to search for products."
    )]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<ProductDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<ProductDto>>), StatusCodes.Status401Unauthorized)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SearchProductsResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedProductResponseExample))]
    public async Task<IResult> SearchProducts(
        [FromQuery] string? searchTerm,
        [FromQuery] decimal? minPrice,
        [FromQuery] decimal? maxPrice,
        [FromQuery] string? orderBy,
        [FromQuery] bool isDescending,
        [FromQuery] int currentPage = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var query = new SearchProductsQuery(searchTerm, minPrice, maxPrice, orderBy, isDescending, currentPage, pageSize);
        var result = await mediator.Send(query, cancellationToken);
        return result.ToResult();
    }

    [HttpGet("category/{categoryId}")]
    [SwaggerOperation(
        Summary = "Get Products by Category",
        Description = "This endpoint allows Admins, Customers and public users to retrieve products by category."
    )]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<ProductDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<ProductDto>>), StatusCodes.Status401Unauthorized)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetProductsByCategoryResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedProductResponseExample))]
    public async Task<IResult> GetProductsByCategory(
        string categoryId,
        [FromQuery] string? orderBy,
        [FromQuery] bool isDescending,
        [FromQuery] int currentPage = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var query = new GetProductsByCategoryQuery(categoryId, orderBy, isDescending, currentPage, pageSize);
        var result = await mediator.Send(query, cancellationToken);
        return result.ToResult();
    }

    [HttpGet("active")]
    [SwaggerOperation(
        Summary = "Get Active Products",
        Description = "This endpoint allows Admins, Customers and public users to retrieve all active products."
    )]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<ProductDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<ProductDto>>), StatusCodes.Status401Unauthorized)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetActiveProductsResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedProductResponseExample))]
    public async Task<IResult> GetActiveProducts(
        [FromQuery] string? orderBy,
        [FromQuery] bool isDescending,
        [FromQuery] int currentPage = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var query = new GetActiveProductsQuery(orderBy, isDescending, currentPage, pageSize);
        var result = await mediator.Send(query, cancellationToken);
        return result.ToResult();
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [SwaggerOperation(
        Summary = "Create New Product",
        Description = "This endpoint allows Admins to create a new product."
    )]
    [ProducesResponseType(typeof(ApiResponse<ProductDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<ProductDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<ProductDto>), StatusCodes.Status401Unauthorized)]
    [SwaggerResponseExample(StatusCodes.Status201Created, typeof(CreatedProductResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(BadRequestProductResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedProductResponseExample))]
    public async Task<IResult> CreateProduct(
        [FromBody] CreateProductCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return result.ToResult();
    }

    [HttpPut("{productId}")]
    [Authorize(Roles = "Admin")]
    [SwaggerOperation(
        Summary = "Update Product",
        Description = "This endpoint allows Admins to update a product."
    )]
    [ProducesResponseType(typeof(ApiResponse<ProductDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<ProductDto>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<ProductDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<ProductDto>), StatusCodes.Status401Unauthorized)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(UpdateProductResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(NotFoundProductResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(BadRequestProductResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedProductResponseExample))]
    public async Task<IResult> UpdateProduct(
        string productId,
        [FromBody] UpdateProductCommand command,
        CancellationToken cancellationToken)
    {
        if (productId != command.ProductId)
        {
            return ApiResponseResults.BadRequest("Product ID mismatch");
        }

        var result = await mediator.Send(command, cancellationToken);
        return result.ToResult();
    }

    [HttpDelete("{productId}")]
    [Authorize(Roles = "Admin")]
    [SwaggerOperation(
        Summary = "Delete Product",
        Description = "This endpoint allows Admins to delete a product."
    )]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status401Unauthorized)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(DeleteProductResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedProductResponseExample))]
    public async Task<IResult> DeleteProduct(
        string productId,
        CancellationToken cancellationToken)
    {
        var command = new DeleteProductCommand(productId);
        var result = await mediator.Send(command, cancellationToken);
        return result.ToResult();
    }
}