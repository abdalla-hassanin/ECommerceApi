using ECommerceApi.Core.Base.Response;
using ECommerceApi.Core.MediatrHandlers.Address;
using Swashbuckle.AspNetCore.Filters;

namespace ECommerceApi.Api.ResponseExample;

public class GetAddressByIdResponseExample : IExamplesProvider<ApiResponse<AddressDto>>
{
    public ApiResponse<AddressDto> GetExamples()
    {
        var addressDto = new AddressDto
        (
            AddressId: "01HF3WFKX1KPY89WNJRXJ6V18N",
            CustomerId: "01HF3WFKX1KPY89WNJRXJ6V18N",
            AddressLine: "123 Main St",
            City: "New York",
            State: "NY",
            Country: "USA",
            Phone: "+1-555-123-4567",
            CreatedAt: DateTime.UtcNow.AddDays(-30),
            UpdatedAt: DateTime.UtcNow
        );

        return ApiResponse<AddressDto>.Factory.Success(addressDto);
    }
}

public class GetAddressesByCustomerIdResponseExample : IExamplesProvider<ApiResponse<IReadOnlyList<AddressDto>>>
{
    public ApiResponse<IReadOnlyList<AddressDto>> GetExamples()
    {
        var addresses = new List<AddressDto>
        {
            new AddressDto(
                AddressId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                CustomerId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                AddressLine: "123 Main St",
                City: "New York",
                State: "NY",
                Country: "USA",
                Phone: "+1-555-123-4567",
                CreatedAt: DateTime.UtcNow.AddDays(-30),
                UpdatedAt: DateTime.UtcNow
            ),
            new AddressDto(
                AddressId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                CustomerId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                AddressLine: "456 Elm St",
                City: "Los Angeles",
                State: "CA",
                Country: "USA",
                Phone: "+1-555-987-6543",
                CreatedAt: DateTime.UtcNow.AddDays(-15),
                UpdatedAt: DateTime.UtcNow
            )
        };

        return ApiResponse<IReadOnlyList<AddressDto>>.Factory.Success(addresses);
    }
}

public class CreatedAddressResponseExample : IExamplesProvider<ApiResponse<AddressDto>>
{
    public ApiResponse<AddressDto> GetExamples()
    {
        var addressDto = new AddressDto
        (
            AddressId: "01HF3WFKX1KPY89WNJRXJ6V18N",
            CustomerId: "01HF3WFKX1KPY89WNJRXJ6V18N",
            AddressLine: "123 Main St",
            City: "New York",
            State: "NY",
            Country: "USA",
            Phone: "+1-555-123-4567",
            CreatedAt: DateTime.UtcNow,
            UpdatedAt: DateTime.UtcNow
        );

        return ApiResponse<AddressDto>.Factory.Created(addressDto, "Address created successfully");
    }
}

public class UpdateAddressResponseExample : IExamplesProvider<ApiResponse<AddressDto>>
{
    public ApiResponse<AddressDto> GetExamples()
    {
        var addressDto = new AddressDto
        (
            AddressId: "01HF3WFKX1KPY89WNJRXJ6V18N",
            CustomerId: "01HF3WFKX1KPY89WNJRXJ6V18N",
            AddressLine: "456 Updated St",
            City: "Los Angeles",
            State: "CA",
            Country: "USA",
            Phone: "+1-555-987-6543",
            CreatedAt: DateTime.UtcNow.AddDays(-30),
            UpdatedAt: DateTime.UtcNow
        );

        return ApiResponse<AddressDto>.Factory.Success(addressDto, "Address updated successfully");
    }
}

public class DeleteAddressResponseExample : IExamplesProvider<ApiResponse<bool>>
{
    public ApiResponse<bool> GetExamples()
    {
        return ApiResponse<bool>.Factory.Success(true, "Address deleted successfully");
    }
}

public class BadRequestAddressResponseExample : IExamplesProvider<ApiResponse<AddressDto>>
{
    public ApiResponse<AddressDto> GetExamples()
    {
        return ApiResponse<AddressDto>.Factory.BadRequest(
            "Invalid address data",
            new List<string> { "AddressLine1 is required", "City must not exceed 50 characters" }
        );
    }
}

public class UnauthorizedAddressResponseExample : IExamplesProvider<ApiResponse<AddressDto>>
{
    public ApiResponse<AddressDto> GetExamples()
    {
        return ApiResponse<AddressDto>.Factory.Unauthorized("Unauthorized access");
    }
}

public class NotFoundAddressResponseExample : IExamplesProvider<ApiResponse<AddressDto>>
{
    public ApiResponse<AddressDto> GetExamples()
    {
        return ApiResponse<AddressDto>.Factory.NotFound("Address not found");
    }
}