using ECommerceApi.Api.ResponseExample;
using ECommerceApi.Api.View;
using ECommerceApi.Core.Base.Response;
using ECommerceApi.Core.MediatrHandlers.Auth;
using ECommerceApi.Core.MediatrHandlers.Auth.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace ECommerceApi.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IMediator mediator) : ControllerBase
{
    [HttpPost("register/customer")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status400BadRequest)]
    [SwaggerResponseExample(StatusCodes.Status201Created, typeof(RegisterCustomerResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(BadRequestAuthResponseExample))]
    public async Task<IResult> RegisterCustomer(
        [FromForm] RegisterCustomerCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return result.ToResult();
    }

    [HttpPost("register/admin")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status400BadRequest)]
    [SwaggerResponseExample(StatusCodes.Status201Created, typeof(RegisterAdminResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(BadRequestAuthResponseExample))]
    public async Task<IResult> RegisterAdmin(
        [FromBody] RegisterAdminCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return result.ToResult();
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(ApiResponse<AuthDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<AuthDto>), StatusCodes.Status401Unauthorized)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(LoginResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedAuthResponseExample))]
    public async Task<IResult> Login(
        [FromBody] LoginCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return result.ToResult();
    }

    [HttpPost("refresh-token")]
    [ProducesResponseType(typeof(ApiResponse<AuthDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<AuthDto>), StatusCodes.Status400BadRequest)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(RefreshTokenResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(BadRequestAuthResponseExample))]
    public async Task<IResult> RefreshToken(
        [FromBody] RefreshTokenCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return result.ToResult();
    }

    [Authorize]
    [HttpPost("revoke-token")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status400BadRequest)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(RevokeTokenResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(BadRequestAuthResponseExample))]
    public async Task<IResult> RevokeToken(
        [FromBody] RevokeTokenCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return result.ToResult();
    }

    [HttpPost("forgot-password")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status400BadRequest)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ForgotPasswordResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(BadRequestAuthResponseExample))]
    public async Task<IResult> ForgotPassword(
        [FromBody] ForgotPasswordCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return result.ToResult();
    }

    [HttpPost("reset-password")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status400BadRequest)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ResetPasswordResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(BadRequestAuthResponseExample))]
    public async Task<IResult> ResetPassword(
        [FromBody] ResetPasswordCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return result.ToResult();
    }

    [HttpGet("reset-password")]
    [ProducesResponseType(typeof(ContentResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
    public IActionResult ResetPasswordPage([FromQuery] string email, [FromQuery] string token)
    {
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(token))
        {
            return BadRequest("Invalid email or token.");
        }

        var htmlContent = ResetPasswordView.GetResetPasswordPage(email, token);

        return Content(htmlContent, "text/html");
    }

    [HttpPost("confirm-email")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status400BadRequest)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ConfirmEmailResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(BadRequestAuthResponseExample))]
    public async Task<IResult> ConfirmEmail(
        [FromBody] ConfirmEmailCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return result.ToResult();
    }

    [HttpGet("confirm-email")]
    [ProducesResponseType(typeof(ContentResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
    public IActionResult ConfirmEmailPage([FromQuery] string userId, [FromQuery] string token)
    {
        var htmlContent = ConfirmEmailView.GetConfirmEmailPage(userId, token);

        return Content(htmlContent, "text/html");
    }

    [HttpPost("resend-email-confirmation")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status400BadRequest)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ResendEmailConfirmationResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(BadRequestAuthResponseExample))]
    public async Task<IResult> ResendEmailConfirmation(
        [FromBody] ResendEmailConfirmationCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return result.ToResult();
    }

    [Authorize]
    [HttpPost("change-password")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status400BadRequest)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ChangePasswordResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(BadRequestAuthResponseExample))]
    public async Task<IResult> ChangePassword(
        [FromBody] ChangePasswordCommand command,
        CancellationToken cancellationToken)
    {
        
        var result = await mediator.Send(command, cancellationToken);
        return result.ToResult();
    }
}