using ECommerceApi.Core.Base.Response;
using ECommerceApi.Core.MediatrHandlers.Admin;
using Swashbuckle.AspNetCore.Filters;

namespace ECommerceApi.Api.ResponseExample;

public class GetAdminByIdResponseExample : IExamplesProvider<ApiResponse<AdminDto>>
{
    public ApiResponse<AdminDto> GetExamples()
    {
        var adminDto = new AdminDto(
            AdminId: "01HF3WFKX1KPY89WNJRXJ6V18N",
            ApplicationUserId: "01HF3WFKX1KPY89WNJRXJ6V18N",
            Position: "Manager",
            FirstName: "John",
            LastName: "Doe",
            Email: "john.doe@example.com"
        );

        return ApiResponse<AdminDto>.Factory.Success(adminDto);
    }
}

public class GetAllAdminsResponseExample : IExamplesProvider<ApiResponse<IReadOnlyList<AdminDto>>>
{
    public ApiResponse<IReadOnlyList<AdminDto>> GetExamples()
    {
        var admins = new List<AdminDto>
        {
            new AdminDto("01HF3WFKX1KPY89WNJRXJ6V18N", "01HF3WFKX1KPY89WNJRXJ6V18N", "Manager", "John", "Doe", "john.doe@example.com"),
            new AdminDto("01HF3WFKX1KPY89WNJRXJ6V18N", "01HF3WFKX1KPY89WNJRXJ6V18N", "Supervisor", "Jane", "Smith", "jane.smith@example.com")
        };

        return ApiResponse<IReadOnlyList<AdminDto>>.Factory.Success(admins);
    }
}


public class UpdateAdminResponseExample : IExamplesProvider<ApiResponse<AdminDto>>
{
    public ApiResponse<AdminDto> GetExamples()
    {
        var adminDto = new AdminDto(
            AdminId: "01HF3WFKX1KPY89WNJRXJ6V18N",
            ApplicationUserId: "01HF3WFKX1KPY89WNJRXJ6V18N",
            Position: "Senior Manager",
            FirstName: "John",
            LastName: "Doe",
            Email: "john.doe@example.com"
        );

        return ApiResponse<AdminDto>.Factory.Success(adminDto, "Admin updated successfully");
    }
}

public class DeleteAdminResponseExample : IExamplesProvider<ApiResponse<bool>>
{
    public ApiResponse<bool> GetExamples()
    {
        return ApiResponse<bool>.Factory.Success(true, "Admin deleted successfully");
    }
}

public class BadRequestAdminResponseExample : IExamplesProvider<ApiResponse<AdminDto>>
{
    public ApiResponse<AdminDto> GetExamples()
    {
        return ApiResponse<AdminDto>.Factory.BadRequest(
            "Invalid admin data",
            new List<string> { "Position is required", "ApplicationUserId is invalid" }
        );
    }
}

public class UnauthorizedAdminResponseExample : IExamplesProvider<ApiResponse<AdminDto>>
{
    public ApiResponse<AdminDto> GetExamples()
    {
        return ApiResponse<AdminDto>.Factory.Unauthorized("Unauthorized access");
    }
}

public class NotFoundAdminResponseExample : IExamplesProvider<ApiResponse<AdminDto>>
{
    public ApiResponse<AdminDto> GetExamples()
    {
        return ApiResponse<AdminDto>.Factory.NotFound("Admin not found");
    }
}