using System.Net;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.Base.Response;

public static class ApiResponseConstants
{
    internal static readonly IReadOnlyList<string> EmptyErrors = Array.Empty<string>();
}

public sealed record ApiResponse<TData>
{
    [JsonPropertyOrder(1)] public required HttpStatusCode StatusCode { get; init; }

    [JsonPropertyOrder(2)]
    public bool IsSuccess => StatusCode is >= HttpStatusCode.OK and < HttpStatusCode.MultipleChoices;

    [JsonPropertyOrder(3)]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Message { get; init; }

    [JsonPropertyOrder(4)]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public IReadOnlyList<string> Errors { get; init; } = ApiResponseConstants.EmptyErrors;

    [JsonPropertyOrder(5)]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public TData? Data { get; init; }

    [JsonPropertyOrder(6)]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public PaginationMetadata? Pagination { get; init; }

    private ApiResponse()
    {
    }

    public static ApiResponse<TData> CreateResponse(HttpStatusCode statusCode, TData? data = default,
        string? message = null,
        IEnumerable<string>? errors = null, PaginationMetadata? pagination = null) =>
        new()
        {
            StatusCode = statusCode,
            Data = data,
            Message = message,
            Errors = errors?.ToList().AsReadOnly() ?? ApiResponseConstants.EmptyErrors,
            Pagination = pagination
        };

    public static class Factory
    {
        private static readonly ILogger<ApiResponse<TData>> Logger =
            LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<ApiResponse<TData>>();

        public static ApiResponse<TData> Success(TData data, string? message = null)
        {
            Logger.LogInformation("Creating Success response with message: {Message}", message);
            return CreateResponse(HttpStatusCode.OK, data, message);
        }

        public static ApiResponse<TData> Created(TData data, string? message = "Resource created successfully")
        {
            Logger.LogInformation("Creating Created response with message: {Message}", message);
            return CreateResponse(HttpStatusCode.Created, data, message);
        }

        public static ApiResponse<TData> NoContent()
        {
            Logger.LogInformation("Creating NoContent response");
            return CreateResponse(HttpStatusCode.NoContent);
        }

        public static ApiResponse<TData> NotFound(string message = "Resource not found")
        {
            Logger.LogWarning("Creating NotFound response with message: {Message}", message);
            return CreateResponse(HttpStatusCode.NotFound, message: message);
        }

        public static ApiResponse<TData> BadRequest(string message, IEnumerable<string>? errors = null)
        {
            Logger.LogWarning("Creating BadRequest response with message: {Message}", message);
            return CreateResponse(HttpStatusCode.BadRequest, message: message, errors: errors);
        }

        public static ApiResponse<TData> Unauthorized(string message = "Unauthorized access")
        {
            Logger.LogWarning("Creating Unauthorized response with message: {Message}", message);
            return CreateResponse(HttpStatusCode.Unauthorized, message: message);
        }

        public static ApiResponse<TData> Forbidden(string message = "Access forbidden")
        {
            Logger.LogWarning("Creating Forbidden response with message: {Message}", message);
            return CreateResponse(HttpStatusCode.Forbidden, message: message);
        }

        public static ApiResponse<TData> ValidationError(IEnumerable<string> errors)
        {
            Logger.LogWarning("Creating ValidationError response with errors: {@Errors}", errors);
            return CreateResponse(HttpStatusCode.UnprocessableEntity, message: "Validation failed", errors: errors);
        }

        public static ApiResponse<TData> ServerError(string message = "An unexpected error occurred")
        {
            Logger.LogError("Creating ServerError response with message: {Message}", message);
            return CreateResponse(HttpStatusCode.InternalServerError, message: message);
        }

        public static ApiResponse<TData> WithPagination(TData data, PaginationMetadata pagination,
            string? message = null)
        {
            Logger.LogInformation("Creating paginated response with message: {Message}", message);
            return CreateResponse(HttpStatusCode.OK, data, message, pagination: pagination);
        }
    }
}

public sealed record PaginationMetadata
{
    public required int CurrentPage { get; init; }
    public required int TotalPages { get; init; }
    public required int PageSize { get; init; }
    public required int TotalCount { get; init; }
    public bool HasPrevious => CurrentPage > 1;
    public bool HasNext => CurrentPage < TotalPages;

    public static PaginationMetadata Create(int currentPage, int totalCount, int pageSize)
    {
        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        return new PaginationMetadata
        {
            CurrentPage = currentPage,
            TotalPages = totalPages,
            PageSize = pageSize,
            TotalCount = totalCount
        };
    }
}

public static class ApiResponseResults
{
    private static readonly ILogger Logger =
        LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger(typeof(ApiResponseResults));

    public static IResult ToResult<T>(this ApiResponse<T> response)
    {
        Logger.LogInformation("Converting ApiResponse to IResult with status code: {StatusCode}", response.StatusCode);
        return Results.Json(response, statusCode: (int)response.StatusCode);
    }

    public static IResult Success<T>(T data, string? message = null)
    {
        Logger.LogInformation("Creating Success IResult with message: {Message}", message);
        return ApiResponse<T>.Factory.Success(data, message).ToResult();
    }

    public static IResult NotFound(string message = "Resource not found")
    {
        Logger.LogWarning("Creating NotFound IResult with message: {Message}", message);
        return ApiResponse<object>.Factory.NotFound(message).ToResult();
    }

    public static IResult BadRequest(string message, IEnumerable<string>? errors = null)
    {
        Logger.LogWarning("Creating BadRequest IResult with message: {Message}", message);
        return ApiResponse<object>.Factory.BadRequest(message, errors).ToResult();
    }

    public static IResult ServerError(string message = "An unexpected error occurred")
    {
        Logger.LogError("Creating ServerError IResult with message: {Message}", message);
        return ApiResponse<object>.Factory.ServerError(message).ToResult();
    }

    public static IResult Unauthorized(string message = "Unauthorized access")
    {
        Logger.LogWarning("Creating Unauthorized IResult with message: {Message}", message);
        return ApiResponse<object>.Factory.Unauthorized(message).ToResult();
    }
}