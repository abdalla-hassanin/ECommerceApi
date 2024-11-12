using ECommerceApi.Core.Base.Response;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.Base.MiddleWare;

public sealed class ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception error)
        {
            logger.LogError(error, "An unhandled exception has occurred while executing the request.");
            await HandleExceptionAsync(context, error);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var response = exception switch
        {
            ValidationException validationException =>
                ApiResponse<object>.Factory.ValidationError(
                    validationException.Errors?.Select(e => e.ErrorMessage) ??
                    [validationException.Message]),

            UnauthorizedAccessException =>
                ApiResponse<object>.Factory.Unauthorized(),

            KeyNotFoundException =>
                ApiResponse<object>.Factory.NotFound(),

            ArgumentException or InvalidOperationException =>
                ApiResponse<object>.Factory.BadRequest(exception.Message),

            _ => ApiResponse<object>.Factory.ServerError(
                "An unexpected error occurred. Please try again later.")
        };

        logger.LogError(exception, "Handled exception: {ExceptionType}. Response: {@Response}", 
            exception.GetType().Name, response);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)response.StatusCode;

        await context.Response.WriteAsJsonAsync(response);
    }
}