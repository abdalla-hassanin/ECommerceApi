using ECommerceApi.Api.ResponseExample;
using ECommerceApi.Core.Base.Response;
using ECommerceApi.Core.MediatrHandlers.ProductVariant;
using ECommerceApi.Core.MediatrHandlers.ProductVariant.Commands;
using ECommerceApi.Core.MediatrHandlers.ProductVariant.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace ECommerceApi.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductVariantController(IMediator mediator) : ControllerBase
{
    [HttpGet("{variantId}")]
    [SwaggerOperation(
        Summary = "Get Product Variant by ID",
        Description = "This endpoint allows Admins, Customers and public users to retrieve a product variant by its ID."
    )]
    [ProducesResponseType(typeof(ApiResponse<ProductVariantDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<ProductVariantDto>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<ProductVariantDto>), StatusCodes.Status401Unauthorized)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetProductVariantByIdResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(NotFoundProductVariantResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedProductVariantResponseExample))]
    public async Task<IResult> GetProductVariantById(
        string variantId,
        CancellationToken cancellationToken)
    {
        var query = new GetProductVariantByIdQuery(variantId);
        var result = await mediator.Send(query, cancellationToken);
        return result.ToResult();
    }

    [HttpGet("product/{productId}")]
    [SwaggerOperation(
        Summary = "Get Product Variants for Product",
        Description = "This endpoint allows Admins, Customers and public users to retrieve product variants for a product."
    )]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<ProductVariantDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<ProductVariantDto>>), StatusCodes.Status401Unauthorized)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetProductVariantsForProductResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedProductVariantResponseExample))]
    public async Task<IResult> GetProductVariantsForProduct(
        string productId,
        CancellationToken cancellationToken)
    {
        var query = new GetProductVariantsForProductQuery(productId);
        var result = await mediator.Send(query, cancellationToken);
        return result.ToResult();
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [SwaggerOperation(
        Summary = "Create Product Variant",
        Description = "This endpoint allows Admins to create a product variant."
    )]
    [ProducesResponseType(typeof(ApiResponse<ProductVariantDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<ProductVariantDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<ProductVariantDto>), StatusCodes.Status401Unauthorized)]
    [SwaggerResponseExample(StatusCodes.Status201Created, typeof(CreatedProductVariantResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(BadRequestProductVariantResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedProductVariantResponseExample))]
    public async Task<IResult> CreateProductVariant(
        [FromBody] CreateProductVariantCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return result.ToResult();
    }

    [HttpPut("{variantId}")]
    [Authorize(Roles = "Admin")]
    [SwaggerOperation(
        Summary = "Update Product Variant",
        Description = "This endpoint allows Admins to update a product variant."
    )]
    [ProducesResponseType(typeof(ApiResponse<ProductVariantDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<ProductVariantDto>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<ProductVariantDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<ProductVariantDto>), StatusCodes.Status401Unauthorized)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(UpdateProductVariantResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(NotFoundProductVariantResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(BadRequestProductVariantResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedProductVariantResponseExample))]
    public async Task<IResult> UpdateProductVariant(
        string variantId,
        [FromBody] UpdateProductVariantCommand command,
        CancellationToken cancellationToken)
    {
        if (variantId != command.VariantId)
        {
            return ApiResponseResults.BadRequest("Variant ID mismatch");
        }

        var result = await mediator.Send(command, cancellationToken);
        return result.ToResult();
    }

    [HttpDelete("{variantId}")]
    [Authorize(Roles = "Admin")]
    [SwaggerOperation(
        Summary = "Delete Product Variant",
        Description = "This endpoint allows Admins to delete a product variant."
    )]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status401Unauthorized)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(DeleteProductVariantResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedProductVariantResponseExample))]
    public async Task<IResult> DeleteProductVariant(
        string variantId,
        CancellationToken cancellationToken)
    {
        var command = new DeleteProductVariantCommand(variantId);
        var result = await mediator.Send(command, cancellationToken);
        return result.ToResult();
    }
}