using ECommerceApi.Api.ResponseExample;
using ECommerceApi.Core.Base.Response;
using ECommerceApi.Core.MediatrHandlers.Category;
using ECommerceApi.Core.MediatrHandlers.Product;
using ECommerceApi.Core.MediatrHandlers.ProductCategory.Commands;
using ECommerceApi.Core.MediatrHandlers.ProductCategory.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace ECommerceApi.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductCategoryController(IMediator mediator) : ControllerBase
{
    [HttpGet("product/{productId}/categories")]
    [SwaggerOperation(
        Summary = "Get Categories for Product",
        Description = "This endpoint allows Admins, Customers and public users to retrieve categories for a product."
    )]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<CategoryDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<CategoryDto>>), StatusCodes.Status401Unauthorized)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetCategoriesForProductResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedProductCategoryResponseExample))]
    public async Task<IResult> GetCategoriesForProduct(
        string productId,
        CancellationToken cancellationToken)
    {
        var query = new GetCategoriesForProductQuery(productId);
        var result = await mediator.Send(query, cancellationToken);
        return result.ToResult();
    }

    [HttpGet("category/{categoryId}/products")]
    [SwaggerOperation(
        Summary = "Get Products for Category",
        Description = "This endpoint allows Admins, Customers and public users to retrieve products for a category."
    )]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<ProductDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<ProductDto>>), StatusCodes.Status401Unauthorized)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetProductsForCategoryResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedProductCategoryResponseExample))]
    public async Task<IResult> GetProductsForCategory(
        string categoryId,
        CancellationToken cancellationToken)
    {
        var query = new GetProductsForCategoryQuery(categoryId);
        var result = await mediator.Send(query, cancellationToken);
        return result.ToResult();
    }

    [HttpPost("product/{productId}/category/{categoryId}")]
    [Authorize(Roles = "Admin")]
    [SwaggerOperation(
        Summary = "Add Product to Category",
        Description = "This endpoint allows Admins to add a product to a category."
    )]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status401Unauthorized)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(AddProductToCategoryResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(BadRequestProductCategoryResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedProductCategoryResponseExample))]
    public async Task<IResult> AddProductToCategory(
        string productId,
        string categoryId,
        CancellationToken cancellationToken)
    {
        var command = new AddProductToCategoryCommand(productId, categoryId);
        var result = await mediator.Send(command, cancellationToken);
        return result.ToResult();
    }

    [HttpDelete("product/{productId}/category/{categoryId}")]
    [Authorize(Roles = "Admin")]
    [SwaggerOperation(
        Summary = "Remove Product from Category",
        Description = "This endpoint allows Admins to remove a product from a category."
    )]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status401Unauthorized)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(RemoveProductFromCategoryResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedProductCategoryResponseExample))]
    public async Task<IResult> RemoveProductFromCategory(
        string productId,
        string categoryId,
        CancellationToken cancellationToken)
    {
        var command = new RemoveProductFromCategoryCommand(productId, categoryId);
        var result = await mediator.Send(command, cancellationToken);
        return result.ToResult();
    }

    [HttpPut("product/{productId}/categories")]
    [Authorize(Roles = "Admin")]
    [SwaggerOperation(
        Summary = "Update Product Categories",
        Description = "This endpoint allows Admins to update categories for a product."
    )]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status401Unauthorized)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(UpdateProductCategoriesResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(BadRequestProductCategoryResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedProductCategoryResponseExample))]
    public async Task<IResult> UpdateProductCategories(
        string productId,
        [FromBody] IEnumerable<string> categoryIds,
        CancellationToken cancellationToken)
    {
        var command = new UpdateProductCategoriesCommand(productId, categoryIds);
        var result = await mediator.Send(command, cancellationToken);
        return result.ToResult();
    }
}