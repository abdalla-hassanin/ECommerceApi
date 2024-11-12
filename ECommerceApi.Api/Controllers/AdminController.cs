using ECommerceApi.Api.ResponseExample;
using ECommerceApi.Core.Base.Response;
using ECommerceApi.Core.MediatrHandlers.Admin;
using ECommerceApi.Core.MediatrHandlers.Admin.Commands;
using ECommerceApi.Core.MediatrHandlers.Admin.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace ECommerceApi.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class AdminController(IMediator mediator) : ControllerBase
{
    [HttpGet("{adminId}")]
    [SwaggerOperation(
        Summary = "Get Admin by ID",
        Description = "This endpoint allows Admins to retrieve an admin by its ID."
    )]
    [ProducesResponseType(typeof(ApiResponse<AdminDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<AdminDto>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<AdminDto>), StatusCodes.Status401Unauthorized)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetAdminByIdResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(NotFoundAdminResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedAdminResponseExample))]
    public async Task<IResult> GetAdminById(
        string adminId,
        CancellationToken cancellationToken)
    {
        var query = new GetAdminByIdQuery(adminId);
        var result = await mediator.Send(query, cancellationToken);
        return result.ToResult();
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get All Admins",
        Description = "This endpoint allows Admins to retrieve all admins."
    )]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<AdminDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<AdminDto>>), StatusCodes.Status401Unauthorized)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetAllAdminsResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedAdminResponseExample))]
    public async Task<IResult> GetAllAdmins(CancellationToken cancellationToken)
    {
        var query = new GetAllAdminsQuery();
        var result = await mediator.Send(query, cancellationToken);
        return result.ToResult();
    }

   
    [HttpPut("{adminId}")]
    [SwaggerOperation(
        Summary = "Update Admin",
        Description = "This endpoint allows Admins to update an admin."
    )]
    [ProducesResponseType(typeof(ApiResponse<AdminDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<AdminDto>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<AdminDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<AdminDto>), StatusCodes.Status401Unauthorized)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(UpdateAdminResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(NotFoundAdminResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(BadRequestAdminResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedAdminResponseExample))]
    public async Task<IResult> UpdateAdmin(
        string adminId,
        [FromBody] UpdateAdminCommand command,
        CancellationToken cancellationToken)
    {
        if (adminId != command.AdminId)
        {
            return ApiResponseResults.BadRequest("Admin ID mismatch");
        }

        var result = await mediator.Send(command, cancellationToken);
        return result.ToResult();
    }

    [HttpDelete("{adminId}")]
    [SwaggerOperation(
        Summary = "Delete Admin",
        Description = "This endpoint allows Admins to delete an admin."
    )]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status401Unauthorized)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(DeleteAdminResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedAdminResponseExample))]
    public async Task<IResult> DeleteAdmin(
        string adminId,
        CancellationToken cancellationToken)
    {
        var command = new DeleteAdminCommand(adminId);
        var result = await mediator.Send(command, cancellationToken);
        return result.ToResult();
    }
}