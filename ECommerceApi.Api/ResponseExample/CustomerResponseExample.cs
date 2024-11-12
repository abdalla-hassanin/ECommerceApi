using ECommerceApi.Core.Base.Response;
using ECommerceApi.Core.MediatrHandlers.Customer;
using Swashbuckle.AspNetCore.Filters;

namespace ECommerceApi.Api.ResponseExample;

public class GetCustomerByIdResponseExample : IExamplesProvider<ApiResponse<CustomerDto>>
{
    public ApiResponse<CustomerDto> GetExamples()
    {
        var customerDto = new CustomerDto
        {
            CustomerId = "01HF3WFKX1KPY89WNJRXJ6V18N",
            ApplicationUserId = "01HF3WFKX1KPY89WNJRXJ6V18N",
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            LastPurchaseDate = DateTime.UtcNow.AddDays(-7),
            DateOfBirth = new DateTime(1990, 1, 1),
            Gender = "Male",
            Bio = "Regular customer",
            ProfilePictureUrl = "https://example.com/profile.jpg",
            Language = "English"
        };

        return ApiResponse<CustomerDto>.Factory.Success(customerDto);
    }
}

public class GetAllCustomersResponseExample : IExamplesProvider<ApiResponse<IReadOnlyList<CustomerDto>>>
{
    public ApiResponse<IReadOnlyList<CustomerDto>> GetExamples()
    {
        var customers = new List<CustomerDto>
        {
            new CustomerDto
            {
                CustomerId = "01HF3WFKX1KPY89WNJRXJ6V18N",
                ApplicationUserId = "01HF3WFKX1KPY89WNJRXJ6V18N",
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                LastPurchaseDate = DateTime.UtcNow.AddDays(-7),
                DateOfBirth = new DateTime(1990, 1, 1),
                Gender = "Male",
                Bio = "Regular customer",
                ProfilePictureUrl = "https://example.com/profile1.jpg",
                Language = "English"
            },
            new CustomerDto
            {
                CustomerId = "01HF3WFKX1KPY89WNJRXJ6V18N",
                ApplicationUserId = "01HF3WFKX1KPY89WNJRXJ6V18N",
                FirstName = "Jane",
                LastName = "Smith",
                Email = "jane.smith@example.com",
                LastPurchaseDate = DateTime.UtcNow.AddDays(-3),
                DateOfBirth = new DateTime(1985, 5, 15),
                Gender = "Female",
                Bio = "VIP customer",
                ProfilePictureUrl = "https://example.com/profile2.jpg",
                Language = "Spanish"
            }
        };

        return ApiResponse<IReadOnlyList<CustomerDto>>.Factory.Success(customers);
    }
}

public class UpdateCustomerResponseExample : IExamplesProvider<ApiResponse<CustomerDto>>
{
    public ApiResponse<CustomerDto> GetExamples()
    {
        var customerDto = new CustomerDto
        {
            CustomerId = "01HF3WFKX1KPY89WNJRXJ6V18N",
            ApplicationUserId = "01HF3WFKX1KPY89WNJRXJ6V18N",
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            LastPurchaseDate = DateTime.UtcNow.AddDays(-1),
            DateOfBirth = new DateTime(1990, 1, 1),
            Gender = "Male",
            Bio = "Updated regular customer",
            ProfilePictureUrl = "https://example.com/updated_profile.jpg",
            Language = "English"
        };


        return ApiResponse<CustomerDto>.Factory.Success(customerDto, "Customer updated successfully");
    }
}

public class DeleteCustomerResponseExample : IExamplesProvider<ApiResponse<bool>>
{
    public ApiResponse<bool> GetExamples()
    {
        return ApiResponse<bool>.Factory.Success(true, "Customer deleted successfully");
    }
}

public class BadRequestCustomerResponseExample : IExamplesProvider<ApiResponse<CustomerDto>>
{
    public ApiResponse<CustomerDto> GetExamples()
    {
        return ApiResponse<CustomerDto>.Factory.BadRequest(
            "Invalid customer data",
            new List<string> { "Date of birth must be in the past", "Gender must not exceed 50 characters" }
        );
    }
}

public class UnauthorizedCustomerResponseExample : IExamplesProvider<ApiResponse<CustomerDto>>
{
    public ApiResponse<CustomerDto> GetExamples()
    {
        return ApiResponse<CustomerDto>.Factory.Unauthorized("Unauthorized access");
    }
}

public class NotFoundCustomerResponseExample : IExamplesProvider<ApiResponse<CustomerDto>>
{
    public ApiResponse<CustomerDto> GetExamples()
    {
        return ApiResponse<CustomerDto>.Factory.NotFound("Customer not found");
    }
}