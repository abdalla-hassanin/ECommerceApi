using ECommerceApi.Core.Base.Response;
using ECommerceApi.Core.MediatrHandlers.Category;
using ECommerceApi.Core.MediatrHandlers.Category.Commands;
using ECommerceApi.Core.MediatrHandlers.Category.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace ECommerceApi.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{categoryId}")]
    [SwaggerOperation(
        Summary = "Get Category by ID",
        Description = "This endpoint allows Admins, Customers and public users to retrieve a category by its ID."
    )]
    [ProducesResponseType(typeof(ApiResponse<CategoryDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<CategoryDto>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<CategoryDto>), StatusCodes.Status401Unauthorized)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetCategoryByIdResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(NotFoundCategoryResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedCategoryResponseExample))]
    public async Task<IResult> GetCategoryById(
        string categoryId,
        CancellationToken cancellationToken)
    {
        var query = new GetCategoryByIdQuery(categoryId);
        var result = await _mediator.Send(query, cancellationToken);
        return result.ToResult();
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get All Categories",
        Description = "This endpoint allows Admins, Customers and public users to retrieve all categories."
    )]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<CategoryDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<CategoryDto>>), StatusCodes.Status401Unauthorized)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetAllCategoriesResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedCategoryResponseExample))]
    public async Task<IResult> GetAllCategories(CancellationToken cancellationToken)
    {
        var query = new GetAllCategoriesQuery();
        var result = await _mediator.Send(query, cancellationToken);
        return result.ToResult();
    }

    [HttpGet("parent/{parentCategoryId}")]
    [SwaggerOperation(
        Summary = "Get Categories by Parent ID",
        Description = "This endpoint allows Admins, Customers and public users to retrieve all categories associated with a specific parent category ID."
    )]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<CategoryDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<CategoryDto>>), StatusCodes.Status401Unauthorized)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetCategoriesByParentIdResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedCategoryResponseExample))]
    public async Task<IResult> GetCategoriesByParentId(
        string parentCategoryId,
        CancellationToken cancellationToken)
    {
        var query = new GetCategoriesByParentIdQuery(parentCategoryId);
        var result = await _mediator.Send(query, cancellationToken);
        return result.ToResult();
    }

    [HttpGet("active")]
    [SwaggerOperation(
        Summary = "Get Active Categories",
        Description = "This endpoint allows Admins, Customers and public users to retrieve all active categories."
    )]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<CategoryDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<CategoryDto>>), StatusCodes.Status401Unauthorized)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetActiveCategoriesResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedCategoryResponseExample))]
    public async Task<IResult> GetActiveCategories(CancellationToken cancellationToken)
    {
        var query = new GetActiveCategoriesQuery();
        var result = await _mediator.Send(query, cancellationToken);
        return result.ToResult();
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [SwaggerOperation(
        Summary = "Create New Category",
        Description = "This endpoint allows Admins to create a new category."
    )]
    [ProducesResponseType(typeof(ApiResponse<CategoryDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<CategoryDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<CategoryDto>), StatusCodes.Status401Unauthorized)]
    [SwaggerResponseExample(StatusCodes.Status201Created, typeof(CreatedCategoryResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(BadRequestCategoryResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedCategoryResponseExample))]
    public async Task<IResult> CreateCategory(
        [FromForm] CreateCategoryCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return result.ToResult();
    }

    [HttpPut("{categoryId}")]
    [Authorize(Roles = "Admin")]
    [SwaggerOperation(
        Summary = "Update Category",
        Description = "This endpoint allows Admins to update a category."
    )]
    [ProducesResponseType(typeof(ApiResponse<CategoryDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<CategoryDto>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<CategoryDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<CategoryDto>), StatusCodes.Status401Unauthorized)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(UpdateCategoryResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(NotFoundCategoryResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(BadRequestCategoryResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedCategoryResponseExample))]
    public async Task<IResult> UpdateCategory(
        string categoryId,
        [FromForm] UpdateCategoryCommand command,
        CancellationToken cancellationToken)
    {
        if (categoryId != command.CategoryId)
        {
            return ApiResponseResults.BadRequest("Category ID mismatch");
        }

        var result = await _mediator.Send(command, cancellationToken);
        return result.ToResult();
    }

    [HttpDelete("{categoryId}")]
    [Authorize(Roles = "Admin")]
    [SwaggerOperation(
        Summary = "Delete Category",
        Description = "This endpoint allows Admins to delete a category."
    )]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status401Unauthorized)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(DeleteCategoryResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedCategoryResponseExample))]
    public async Task<IResult> DeleteCategory(
        string categoryId,
        CancellationToken cancellationToken)
    {
        var command = new DeleteCategoryCommand(categoryId);
        var result = await _mediator.Send(command, cancellationToken);
        return result.ToResult();
    }
}