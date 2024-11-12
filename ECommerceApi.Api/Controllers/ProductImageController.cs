using ECommerceApi.Api.ResponseExample;
using ECommerceApi.Core.Base.Response;
using ECommerceApi.Core.MediatrHandlers.ProductImage;
using ECommerceApi.Core.MediatrHandlers.ProductImage.Commands;
using ECommerceApi.Core.MediatrHandlers.ProductImage.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace ECommerceApi.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductImageController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductImageController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{imageId}")]
    [SwaggerOperation(
        Summary = "Get Product Image by ID",
        Description = "This endpoint allows Admins, Customers and public users to retrieve a product image by its ID."
    )]
    [ProducesResponseType(typeof(ApiResponse<ProductImageDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<ProductImageDto>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<ProductImageDto>), StatusCodes.Status401Unauthorized)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetProductImageByIdResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(NotFoundProductImageResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedProductImageResponseExample))]
    public async Task<IResult> GetProductImageById(
        string imageId,
        CancellationToken cancellationToken)
    {
        var query = new GetProductImageByIdQuery(imageId);
        var result = await _mediator.Send(query, cancellationToken);
        return result.ToResult();
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get All Product Images",
        Description = "This endpoint allows Admins, Customers and public users to retrieve all product images."
    )]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<ProductImageDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<ProductImageDto>>), StatusCodes.Status401Unauthorized)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetAllProductImagesResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedProductImageResponseExample))]
    public async Task<IResult> GetAllProductImages(CancellationToken cancellationToken)
    {
        var query = new GetAllProductImagesQuery();
        var result = await _mediator.Send(query, cancellationToken);
        return result.ToResult();
    }

    [HttpGet("product/{productId}")]
    [SwaggerOperation(
        Summary = "Get Product Images for Product",
        Description = "This endpoint allows Admins, Customers and public users to retrieve all product images for a product."
    )]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<ProductImageDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<ProductImageDto>>), StatusCodes.Status401Unauthorized)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetProductImagesForProductResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedProductImageResponseExample))]
    public async Task<IResult> GetProductImagesForProduct(
        string productId,
        CancellationToken cancellationToken)
    {
        var query = new GetProductImagesForProductQuery(productId);
        var result = await _mediator.Send(query, cancellationToken);
        return result.ToResult();
    }

    [HttpGet("variant/{variantId}")]
    [SwaggerOperation(
        Summary = "Get Product Images for Variant",
        Description = "This endpoint allows Admins, Customers and public users to retrieve all product images for a variant."
    )]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<ProductImageDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<ProductImageDto>>), StatusCodes.Status401Unauthorized)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetProductImagesForVariantResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedProductImageResponseExample))]
    public async Task<IResult> GetProductImagesForVariant(
        string variantId,
        CancellationToken cancellationToken)
    {
        var query = new GetProductImagesForVariantQuery(variantId);
        var result = await _mediator.Send(query, cancellationToken);
        return result.ToResult();
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [SwaggerOperation(
        Summary = "Create New Product Image",
        Description = "This endpoint allows Admins to create a new product image."
    )]
    [ProducesResponseType(typeof(ApiResponse<ProductImageDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<ProductImageDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<ProductImageDto>), StatusCodes.Status401Unauthorized)]
    [SwaggerResponseExample(StatusCodes.Status201Created, typeof(CreatedProductImageResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(BadRequestProductImageResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedProductImageResponseExample))]
    public async Task<IResult> CreateProductImage(
        [FromForm] CreateProductImageCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return result.ToResult();
    }

    [HttpPut("{imageId}")]
    [Authorize(Roles = "Admin")]
    [SwaggerOperation(
        Summary = "Update Product Image",
        Description = "This endpoint allows Admins to update a product image."
    )]
    [ProducesResponseType(typeof(ApiResponse<ProductImageDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<ProductImageDto>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<ProductImageDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<ProductImageDto>), StatusCodes.Status401Unauthorized)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(UpdateProductImageResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(NotFoundProductImageResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(BadRequestProductImageResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedProductImageResponseExample))]
    public async Task<IResult> UpdateProductImage(
        string imageId,
        [FromForm] UpdateProductImageCommand command,
        CancellationToken cancellationToken)
    {
        if (imageId != command.ImageId)
        {
            return ApiResponseResults.BadRequest("Image ID mismatch");
        }

        var result = await _mediator.Send(command, cancellationToken);
        return result.ToResult();
    }

    [HttpDelete("{imageId}")]
    [Authorize(Roles = "Admin")]
    [SwaggerOperation(
        Summary = "Delete Product Image",
        Description = "This endpoint allows Admins to delete a product image."
    )]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status401Unauthorized)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(DeleteProductImageResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedProductImageResponseExample))]
    public async Task<IResult> DeleteProductImage(
        string imageId,
        CancellationToken cancellationToken)
    {
        var command = new DeleteProductImageCommand(imageId);
        var result = await _mediator.Send(command, cancellationToken);
        return result.ToResult();
    }
}